using System.Collections.Generic;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class RingViewModel
    {
        public string Label { get; set; }
        public string ShortLabel { get; set; }
        public string ThemeColor { get; set; }
        public string ThemeCssColor{ get; set; }
        public List<RingCardViewModel> Cards { get; set; }
    }
}