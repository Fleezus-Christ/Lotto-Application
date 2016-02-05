using System.Collections.Generic;

namespace LottoGenerator.Model
{
    public class EuromillionViewModel
    {
        public IEnumerable<int> NumberList { get; set; }
        public IEnumerable<int> NumberLuckyList { get; set; }
        public int MatchCount { get; set; }
    }
}
