using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LottoGenerator.Model;

namespace LottoGenerator.View
{
    /// <summary>
    /// Interaction logic for EuromillionsView.xaml
    /// </summary>
    public partial class EuromillionsView : UserControl
    {
        public List<EuromillionViewModel> GeneratedPickList;

        public EuromillionsView()
        {
            InitializeComponent();
        }

        private void GenerateButton1_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(100);
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void GenerateButton2_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(1000);
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void GenerateButton3_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(10000);
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void GenerateButton4_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(10000);
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void GenerateButton5_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(100000);
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void ProcessButton_OnClick(object sender, RoutedEventArgs e)
        {
            var winningDraw = new List<int>
            {
                Convert.ToInt32(Draw1.Text),
                Convert.ToInt32(Draw2.Text),
                Convert.ToInt32(Draw3.Text),
                Convert.ToInt32(Draw4.Text),
                Convert.ToInt32(Draw5.Text)
            };

            for (var i = 0; i < GeneratedPickList.Count; i++)
            {
                var newList = GeneratedPickList[i].NumberList.Except(winningDraw).ToList();
                GeneratedPickList[i].MatchCount = GeneratedPickList[i].NumberList.Count() - newList.Count();
            }

            // sort list
            GeneratedPickList = GeneratedPickList = GeneratedPickList.OrderByDescending(o => o.MatchCount).ToList();
            LottoList.ItemsSource = GeneratedPickList;
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            LottoList.ItemsSource = new List<EuromillionViewModel>();
            Draw1.Text = "";
            Draw2.Text = "";
            Draw3.Text = "";
            Draw4.Text = "";
            Draw5.Text = "";
            DrawLucky1.Text = "";
            DrawLucky2.Text = "";
        }

        private void LottoList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private static EuromillionViewModel GeneratePick()
        {
            var numbers = Enumerable.Range(1, 50).ToList();
            var luckyNumbers = Enumerable.Range(1, 11).ToList();
            var index = -1;

            for (var i = 0; i < 45; i++)
            {
                index = RandomNumber(0, numbers.Count);
                numbers.RemoveAt(index);
            }

            for (var i = 0; i < 9; i++)
            {
                index = RandomNumber(0, luckyNumbers.Count);
                luckyNumbers.RemoveAt(index);
            }

            return new EuromillionViewModel()
            {
                NumberList = numbers,
                NumberLuckyList = luckyNumbers
            };
        }

        private static List<EuromillionViewModel> GeneratedPick(int total)
        {
            var generatedPick = new List<EuromillionViewModel>();
            for (var i = 0; i < total; i++)
            {
                generatedPick.Add(GeneratePick());
            }
            return generatedPick;
        }

        private void AcceptedTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private static int RandomNumber(int min, int max)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[8];
            rng.GetBytes(buffer);
            var result = BitConverter.ToInt32(buffer, 0);
            return new Random(result).Next(min, max);
        }
    }
}
