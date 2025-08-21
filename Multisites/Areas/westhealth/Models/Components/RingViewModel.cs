using System.Collections.Generic;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class RingViewModel
    {
        public string ringLabel { get; set; }
        public string ringShortLabel { get; set; }
        public string themeColor { get; set; }
        public string ThemeCssColor{ get; set; }
        public List<RingCardViewModel> Cards { get; set; }
    }
}