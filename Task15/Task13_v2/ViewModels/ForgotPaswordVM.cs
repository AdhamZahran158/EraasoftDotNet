using System.ComponentModel.DataAnnotations;

namespace Task13_v2.ViewModels
{
    public class ForgotPaswordVM
    {
        public int Id { get; set; }
        [Required]
        public string EmailOrUsername { get; set; } = string.Empty;
    }
}
