using System.ComponentModel.DataAnnotations;

namespace Task13_v2.ViewModels
{
    public class RegisterVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword {  get; set; }
        public string? Address { get; set; }
    }
}
