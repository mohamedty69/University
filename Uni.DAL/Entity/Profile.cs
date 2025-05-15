using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Profile
    {
        [Key]

        public string Gender { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Student")]
        public string Id { get; set; }

        public virtual Student Student { get; set; }


    }
}
