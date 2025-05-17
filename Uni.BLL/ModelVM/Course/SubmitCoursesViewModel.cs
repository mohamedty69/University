using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.BLL.ModelVM.Course
{
    public class SubmitCoursesViewModel
    {
        public List<string> SelectedCourseCodes { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
    }
}
