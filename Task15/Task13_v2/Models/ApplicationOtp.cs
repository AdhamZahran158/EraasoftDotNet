using System.ComponentModel;

namespace Task13_v2.Models
{
    public class ApplicationOtp
    {
        public int Id { get; set; }
        public int Otp {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ValidTo { get; set; } = DateTime.Now.AddHours(1);
        [DefaultValue(true)]
        public bool IsValid { get; set; } = true;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
