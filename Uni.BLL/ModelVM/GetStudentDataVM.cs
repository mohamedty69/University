﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.BLL.ModelVM.Data
{
	public class GetStudentDataVM
	{
		[Key]
		public string Id { get; set; }
		public string? FirstName { get; set; }

		public string? Email { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? PhoneNumber { get; set; }
		public string? NationalId { get; set; }
		public string? Address { get; set; }
	}
}
