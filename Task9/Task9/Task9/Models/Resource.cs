using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Models
{
    //[PrimaryKey(nameof(ResourceId))]
    internal class Resource
    {
        [Key, Required]
        public int ResourceId { get; set; }
        [Required, MaxLength(50), Unicode(true)]
        public string Name { get; set; }
        [Unicode(false)]
        public string Url { get; set; }
        public enum ResourceType
        {
            video,
            presentation,
            document,
            other
        }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        Course Course { get; set; } = default!;
    }
}
