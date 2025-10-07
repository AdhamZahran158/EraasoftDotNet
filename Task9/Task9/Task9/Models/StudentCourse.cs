using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Models
{
    [PrimaryKey(nameof(StudentId),nameof(CourseId))]
    internal class StudentCourse
    {
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        Student Student { get; set; } = default!;
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        Course Course { get; set; } = default!;
    }
}
