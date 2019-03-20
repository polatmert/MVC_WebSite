using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class TestData
    {
        public TestData()
        {
            MyEverNote.DataAcessLayer.DatabaseContext db = new MyEverNote.DataAcessLayer.DatabaseContext();
            db.Categories.ToList();
        }
    }
}
