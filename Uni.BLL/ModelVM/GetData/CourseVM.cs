using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.BLL.ModelVM.GetDataVM
{
    public class CourseVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
    }
}
