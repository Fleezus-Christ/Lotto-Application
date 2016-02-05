using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LottoGenerator.Model
{
    public class EuromillionViewModel : INotifyPropertyChanged
    {
        public IEnumerable<int> NumberList { get; set; }
        public IEnumerable<int> NumberLuckyList { get; set; }
        private int _matchCount;

        public EuromillionViewModel()
        {
            _matchCount = 0;
        }

        public int MatchCount
        {
            get { return _matchCount; }
            set { _matchCount = value; InvokePropertyChanged("MatchCount"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            var changed = PropertyChanged;
            changed?.Invoke(this, e);
        }
    }
}