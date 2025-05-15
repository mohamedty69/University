using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;
using Uni.DAL.Enum;

namespace Uni.BLL.ModelVM
{
    public class CourseSelectionViewModel
    {
        public string SelectedDepartmentId { get; set; }
        public string SelectedLevel { get; set; }
        public string SelectedSemester { get; set; }

        public List<string> SelectedSubjectIds { get; set; }

        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> AvailableSubjects { get; set; }
    }
}
