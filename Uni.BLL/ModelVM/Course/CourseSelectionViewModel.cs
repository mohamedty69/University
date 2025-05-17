using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;

namespace Uni.BLL.ModelVM.Course
{
    public class CourseSelectionViewModel
    {
        public List<Department> Departments { get; set; }
        public List<string> Levels { get; set; }
        public List<string> Semesters { get; set; }
    }

}
