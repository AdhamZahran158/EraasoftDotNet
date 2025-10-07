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
    [PrimaryKey(nameof(StudentId))]
    internal class Student
    {
        //[Key, Required]
        public int StudentId { get; set; }
        [MaxLength(100), Unicode(true), Required]
        public string Name { get; set; }
        [Length(10,10),  Unicode(false)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegisteredOn { get; set; }
        [AllowNull]
        public DateTime Birthday { get; set; }
    }
}
