﻿using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.BLL.ModelVM.Account
{
    public class LoginVM
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool? RememberMe { get; set; }
        public IEnumerable<AuthenticationScheme>? Schemes { get; set; }
    }
}
