using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Prerequisites
    {
        [Key, Column(Order = 0)]
        [ForeignKey("MainCourse")]

        public string CourseId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("PrerequisiteCourse")]

        public string PrerequisiteId { get; set; }
      
        public virtual Course MainCourse { get; set; }
        public virtual Course PrerequisiteCourse { get; set; }
    }
}
