using System.Collections.Generic;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class RingDiagramViewModel
    {
        public string Title { get; set; }
        public string Copy { get; set; }
        public List<RingViewModel> Rings { get; set; }

    }
}