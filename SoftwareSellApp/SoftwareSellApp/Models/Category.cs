﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSellApp.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}