using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.BLL.ModelVM
{
    public class AvailableCoursesViewModel
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        public bool IsSelected { get; set; }
    }
}
