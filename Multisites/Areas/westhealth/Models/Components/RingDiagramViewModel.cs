using System.Collections.Generic;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class RingDiagramViewModel
    {
        public string introTitle { get; set; }
        public string introCopy { get; set; }
        public List<RingViewModel> Rings { get; set; }

    }
}