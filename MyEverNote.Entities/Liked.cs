using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{
    [Table("Likes")]
    public class Liked
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary Key and Autoincrement
        public int Id { get; set; }

        public virtual Note Note { get; set; }

        public virtual EverNoteUser LikedUser { get; set; }
    }
}
