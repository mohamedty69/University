using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.DAL.Entity;
using AutoMapper;
using System.Numerics;
using Uni.BLL.ModelVM.Data;

namespace Uni.BLL.Mapping
{
        public class DomainProfile : AutoMapper.Profile
        {
            public DomainProfile()
            {
                // User Mapper
                CreateMap<Student, CreateStudentVM>().ReverseMap();
                CreateMap<LoginVM, Student>().ReverseMap();
			    CreateMap<Course, CourseVM>();
                CreateMap<Student,GetStudentDataVM>().ReverseMap();
                CreateMap<Department, DepartmentVM>();
                CreateMap<Takes, TakesVM>().ReverseMap();
                CreateMap<Instructor, InstructorVM>().ReverseMap();
                CreateMap<Teaches, TeachesVM>().ReverseMap();
                CreateMap<Rcords,RcordsVM>().ReverseMap();


		}
	}
}
