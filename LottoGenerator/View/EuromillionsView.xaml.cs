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

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            GeneratedPickList = GeneratedPick(3);
            LottoList.ItemsSource = GeneratedPickList;
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
                NumberLuckyList = luckyNumbers,
                MatchCount = 0
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

        private void ProcessButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void LottoList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
