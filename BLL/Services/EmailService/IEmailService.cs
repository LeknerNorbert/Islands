﻿using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);
    }
}
