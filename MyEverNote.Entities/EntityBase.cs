using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEverNote.Entities
{
    public class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary Key and Autoincrement
        public int  Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required , StringLength(30)]
        public string ModifiedUsername { get; set; }
    }
}
