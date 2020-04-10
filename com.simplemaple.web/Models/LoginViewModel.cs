﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace com.simplemaple.web.Models
{
    public class LoginViewModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string Captcha { get; set; }
    }
}
