using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Models
{
    //[PrimaryKey(nameof(HomeworkId))]
    internal class HomeWork
    {
        [Key, Required]
        public int HomeworkId { get; set; }
        [Required, Unicode(false)]
        public string Content { get; set; }
        public enum ContentType
        {
            Application,
            pdf,
            Zip
        }
        public DateTime SubmissionTime { get; set; }
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = default!;
    }
}
