using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM.Data;

namespace Uni.BLL.ModelVM
{
	public class StudentAllData
	{
		private List<RcordsVM>? records;

		public List<GetStudentDataVM> Students { get; set; }
		public List<CourseVM> Courses { get; set; }
		public List<DepartmentVM> Departments { get; set; }
		public List<TakesVM> Takes { get; set; }
		public List<InstructorVM> Instructors { get; set; }
		public List<TeachesVM> Teaches { get; set; }
		List<RcordsVM>? Rcords { get; set; }
	}
}
