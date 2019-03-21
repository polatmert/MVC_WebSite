 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEverNote.Entities;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class Initializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        //Adding admin User
        protected override void Seed(DatabaseContext context)
        {
            EverNoteUser admin = new EverNoteUser()
            {
                Name = "Mert",
                Surname = "Polat",
                Email = "mertpolat@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "mertpolat",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(10),
                ModifiedUsername = "mertpolat"
            };

            //Adding Standart User
            EverNoteUser standartUser = new EverNoteUser()
            {
                Name = "Boran",
                Surname = "Kaya",
                Email = "boran@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "borankaya",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(10),
                ModifiedUsername = "borankaya"
            };

            for (int i = 0; i < 8; i++)
            {
                EverNoteUser user = new EverNoteUser()
                    {
                        Name = FakeData.NameData.GetFirstName(),
                        Surname = FakeData.NameData.GetSurname(),
                        Email = FakeData.NetworkData.GetEmail(),
                        ActivateGuid = Guid.NewGuid(),
                        IsActive = true,
                        IsAdmin = true,
                        Username = "user" + i,
                        Password = "123",
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = "user" + i,
                    };

                context.EvernoteUsers.Add(user);
            }

            context.SaveChanges();

            //User list for using..
            List<EverNoteUser> userlist = context.EvernoteUsers.ToList();

            //Adding Fake categories..
            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "mertpolat"
                };

                context.Categories.Add(category);

                //Adding fake  notes..
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    EverNoteUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category = category,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,
                    };

                    category.Notes.Add(note);

                    //Adding fake comments..
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EverNoteUser commentOwner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentences(4),
                            Owner = commentOwner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername =commentOwner.Username,
                        };

                        note.Comments.Add(comment);
                    }
                 
                    //Adding fake likes..
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };

                        note.Likes.Add(liked);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
