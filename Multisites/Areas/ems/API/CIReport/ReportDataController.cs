using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.ems.Models;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace NM_MultiSites.Areas.ems.API
{
    public class ReportDataController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        private Item[] getCIPdf(String userDetails)
        {
            StringBuilder fastQuery = new StringBuilder();
            fastQuery.Append("fast:");
            /*
             *get only pdf from the paths /sitecore/Media Library/NMPN/cireport
             * /sitecore/templates/System/Media/Unversioned/Pdf {0603F166-35B8-469F-8123-E8D87BEDC171}
             * /sitecore/templates/System/Media/versioned/Pdf {CC80011D-8EAE-4BFC-84F1-67ECD0223E9E}
             */
            fastQuery.Append("/sitecore/Media Library/NMPN/cireport//*[(@@templateid='{0603F166-35B8-469F-8123-E8D87BEDC171}' or @@templateid='{CC80011D-8EAE-4BFC-84F1-67ECD0223E9E}') ");
            if (!String.Equals("ADM", userDetails))
            {
                /*
                 * if not admin, get pdf based on NPI associated to their login.
                 * Admin will see all pdfs
                 */
                fastQuery.Append("and (");
                int i = 0;
                foreach (String npi in userDetails.Split(','))
                {
                    if (!String.IsNullOrWhiteSpace(npi.Trim()))
                    {
                        if (i > 0)
                        {
                            fastQuery.Append(" or ");
                        }
                        i++;
                        fastQuery.Append("@@name='");
                        fastQuery.Append(npi.Trim());
                        fastQuery.Append("' ");
                    }
                }
                fastQuery.Append(" ) ");
            }
            fastQuery.Append("]");
            return Sitecore.Context.Database.SelectItems(fastQuery.ToString());

        }

        [HttpGet]
        public CIReport Index()
        {

            String userDetails = Page.GetCurrentUserNPI();
            ArrayList data = new ArrayList();
            var ciReport = new CIReport();
            if (!String.IsNullOrWhiteSpace(userDetails))
            {                
                //get all applicatable pdfs using fast query
                var childs = getCIPdf(userDetails);                
                if (childs != null)
                {
                    StringBuilder sb = new StringBuilder();
                    String[] path = null;
                    String month = "";
                    String title = "";
                   // ArrayList pdfItem = null;
                    foreach (Item child in childs)
                    {
                        //pdfItem = new ArrayList();
                        month = "";
                        title = child.Name+" CI Report ";
                        path = child.Paths.Path.Split('/');
                        try
                        {
                            title = title + path[path.Length - 3] + "-" + path[path.Length - 2];
                        }
                        catch (Exception e)
                        {
                            Log.Info(" Exception in forming pdf title" + child.Paths.Path + " : " + e, this);
                        }
                       
                        try
                        {
                            month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Int32.Parse(path[path.Length - 2])) + ", ";
                            month = month + path[path.Length - 3];
                        }
                        catch (Exception e)
                        {
                            Log.Info(" Exception in coverting to date" + child.Paths.Path + " : " + e, this);
                        }
                        ciReport.Add(
                          new CIItem()
                          {
                              Title = title,
                              Date = month,
                              Npi = child.Name,
                              Url=MediaManager.GetMediaUrl(child)
                          });
                        //pdfItem.Add(child.Name);
                       // pdfItem.Add(month + path[path.Length - 3]);
                       // data.Add(pdfItem);
                    }
                }
            }
            return ciReport;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}