﻿using Novolog.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novolog.Application.BL
{
    public static class ContactUs
    {
        static public Queue<ContactUsDto> contacts = new Queue<ContactUsDto>();
    }
}
