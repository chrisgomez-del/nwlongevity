using NM_MultiSites.Areas.Innovation.Infrastructure.Caching;


namespace NM_MultiSites.Areas.Innovation
{
    public class BaseService
    {
        protected ICacheClient Cache { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
        {
            Cache = new MemoryCacheClient();
        }
    }
}
