using System.Configuration;

namespace NM_MultiSites.Areas.Innovation.Infrastructure.Logging
{
    public class DebugLogger
    {
        public static void LogMessage(string message, string owner = "Innovation.Areas.Innovation.Infrastructure.Logging.DebugLogger") {
            var useVerboseLogging = false;
            bool.TryParse(ConfigurationManager.AppSettings["System:UseVerboseDebugLogging"], out useVerboseLogging);

            if (useVerboseLogging)
            {
                Sitecore.Diagnostics.Log.Info(message, owner);
            }
        }
    }
}
