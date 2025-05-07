using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.DAL.Entity;
using AutoMapper;

namespace Uni.BLL.Mapping
{
        public class DomainProfile : AutoMapper.Profile
        {
            public DomainProfile()
            {
                // User Mapper
                CreateMap<Student, CreateStudentVM>().ReverseMap();
                CreateMap<LoginVM, Student>().ReverseMap();
        
            }
        }
}
