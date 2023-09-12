﻿using System.ComponentModel.DataAnnotations;

namespace ImplementSP.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
