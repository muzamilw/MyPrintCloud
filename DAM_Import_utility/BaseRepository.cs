using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcher
{
    public abstract class BaseRepository<TDomainClass> where TDomainClass : class 
    {
        protected BaseRepository()
        {
            
        }

        public abstract long Create();
        


    
        

        public long Update()
        {
            long upd = 11;
            return upd;
        }
            
    }
}
