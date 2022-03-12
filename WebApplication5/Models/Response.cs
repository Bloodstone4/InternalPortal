﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Response
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string ImageLink { get; set; }
    }
}
