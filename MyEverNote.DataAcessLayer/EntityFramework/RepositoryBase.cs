using MyEverNote.DataAccessLayer;
using MyEverNote.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    //Singleton Design Pattern 
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (context == null)
            {
                //Prevents 2 threads from running at the same time
                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }
    }
}
