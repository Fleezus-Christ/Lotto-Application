using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<EuromillionViewModel> _generatedPick;

        public EuromillionsView()
        {
            InitializeComponent();
        }

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _generatedPick = GeneratedPick(10);
            LottoList.ItemsSource = _generatedPick;
        }

        private static EuromillionViewModel GeneratePick()
        {
            var numbers = Enumerable.Range(1, 50).ToList();
            var luckyNumbers = Enumerable.Range(1, 11).ToList();
            var random = new Random();
            var index = -1;

            for (var i = 0; i < 45; i++)
            {
                index = random.Next(numbers.Count);
                numbers.RemoveAt(index);
            }

            for (var i = 0; i < 9; i++)
            {
                index = random.Next(luckyNumbers.Count);
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
    }
}
