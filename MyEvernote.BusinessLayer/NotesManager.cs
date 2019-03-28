using MyEvernote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class NotesManager
    {
        private Repository<Note> repoNote = new Repository<Note>();

        public List<Note> GetAllNotes()
        {
            return repoNote.List();
        }
    }
}
