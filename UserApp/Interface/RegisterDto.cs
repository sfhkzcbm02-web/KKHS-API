﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHS.Service.UserApp
{
    public class RegisterDto
    {

        public string UserName { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
