using System.ComponentModel.DataAnnotations;

namespace Task13_v2.ViewModels
{
    public class ResetPaswordVM
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
