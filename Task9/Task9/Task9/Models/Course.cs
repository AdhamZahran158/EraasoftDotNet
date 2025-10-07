using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Models
{
    //[PrimaryKey(nameof(CourseId))]
    internal class Course
    {
        [Key, Required]
        public int CourseId { get; set; }
        [MaxLength(80), Required, Unicode(true)]
        public string Name { get; set; }
        [Unicode(true), AllowNull]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
