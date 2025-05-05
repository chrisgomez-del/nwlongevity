using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.ems.Models  
{
    public class Block : RenderingModel
    {
        private Sitecore.Mvc.Presentation.Rendering _rendering;
        public Item item { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString ShortText { get; set; }
        public HtmlString MainContent { get; set; }

        public HtmlString Image { get; set; }
        public Block()
        {
            this.Initialize(RenderingContext.Current.Rendering);
        }

        public override void Initialize(Sitecore.Mvc.Presentation.Rendering rendering)
        {
            base.Initialize(rendering);
            _rendering = rendering;
            item = Item;
            Title = new HtmlString(FieldRenderer.Render(item, "Title"));
            ShortText = new HtmlString(FieldRenderer.Render(item, "ShortText")); 
            MainContent = new HtmlString(FieldRenderer.Render(item, "MainContent"));
            Image = new HtmlString(FieldRenderer.Render(item, "Image"));


        }
    }
}