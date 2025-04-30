using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NM_MultiSites.Areas.Innovation.Infrastructure.Caching
{
    public interface ICustomCache<T>
    {
        T Create();
        T Read();
        void Update();
        void Delete();
    }
}