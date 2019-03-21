using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class TestData
    {
        private Repository<EverNoteUser> repoUser = new Repository<EverNoteUser>();
        private Repository<Category> repoCategory = new Repository<Category>();
        private Repository<Comment> repoComment = new Repository<Comment>();
        private Repository<Note> repoNote = new Repository<Note>();

        public TestData()
        {
            List<Category> categories = repoCategory.List();
            //List<Category> categories_filtered = repoCategory.List(x => x.Id > 5);
        }

        public void InsertTest()
        {
            int result = repoUser.Insert(new EverNoteUser()
            {
                Name = "aaa",
                Surname = "bb",
                Email = "aa@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aabb",
                Password = "111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "aabb"
            });
        }

        public void UpdateTest()
        {
            EverNoteUser user = repoUser.Find(x => x.Username == "aabb");
            if (user != null)
            {
                user.Username = "xxx";
                int result = repoUser.Update(user);
            }
        }

        public void DeleteTest()
        {
            EverNoteUser user = repoUser.Find(x => x.Username == "xxx");
            if (user != null)
            {
                int result = repoUser.Delete(user);
            }
        }

        public void CommentTest()
        {
            EverNoteUser user = repoUser.Find(x => x.Id == 1);
            Note note = repoNote.Find(x => x.Id == 1);
            Comment comment = new Comment()
            {
                Text = "Test Comment",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "mertpolat",
                Note = note,
                Owner = user
            };
            repoComment.Insert(comment);
        }
    }
}