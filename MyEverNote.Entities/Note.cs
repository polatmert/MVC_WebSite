using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{
    [Table("Notes")]
    public class Note : EntityBase
    {
        [Required, StringLength(500)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Text { get; set; }

        public bool IsDraft { get; set; }

        public int LikeCount { get; set; }

        public int CategoryId { get; set; }

        public virtual EverNoteUser Owner { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Liked> Likes { get; set; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
