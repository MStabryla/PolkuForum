using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PolkuForum.Models
{
    public class CreateUser
    {
        [Required]
        [Display(Name = "Podaj login")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z,a-z]+[^!@#$%^&*()<>;'\|]*",ErrorMessage ="Login musi zaczynać się literą oraz nie zawierać żadnych specjalnych znaków")]
        public string Login
        { get; set; }
        [Required]
        [Display(Name = "Twój email")]
        [EmailAddress]
        public string Email
        { get; set; }
        [Required]
        [Display(Name = "Podaj nick")]
        [StringLength(50,ErrorMessage = "Za długi nick - max 50 znaków")]
        public string Nick
        { get; set; }
        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password
        { get; set; }
        [Required]
        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword
        { get; set; }
        [Display(Name = "Wybierz swoj zdjęcie profilowe")]
        public string Image
        { get; set; }
        [Display(Name = "Wprowadź opis")]
        public string Description
        { get; set; }
        [Display(Name = "Twoje wykształcenie i osiągnięcia etc.")]
        public string Education
        { get; set; }
    }
}