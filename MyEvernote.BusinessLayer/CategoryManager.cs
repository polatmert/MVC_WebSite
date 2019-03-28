using MyEvernote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager
    {
        private Repository<Category> repoCategory = new Repository<Category>();

        public List<Category> GetCategories()
        {
            return repoCategory.List();
        }

        public Category GetCategoryById(int id)
        {
            return repoCategory.Find(x => x.Id == id);
        }
    }
}
