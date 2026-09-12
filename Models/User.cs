using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SouvenirShop.Models{

    public class User
    {
        public int? id {get; set; }
        public string? Username {get; set; }
        public string? Email {get; set; }
        public string? Password {get; set; }
        public int[]? Corzina {get; set;}

        [NotMapped]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string Repeat_password {get; set;}
    }
}