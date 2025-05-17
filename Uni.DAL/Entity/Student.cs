using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Student : IdentityUser 
    {
        [Key]
      //  public string SId { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
       // public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Takes> Takes { get; set; }
        public virtual ICollection<Rcords> Records { get; set; }
        public virtual AcademicStatus AcademicStatus { get; set; }


    }
}
