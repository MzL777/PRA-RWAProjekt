using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Niste upisali E-mail!")]
        [StringLength(127, ErrorMessage = "Maksimalna duljina E-mail adrese je 127 znakova!")]
        [EmailAddress(ErrorMessage = "Neispravna E-mail adresa!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Niste upisali lozinku!")]
        [StringLength(100, ErrorMessage = "Maksimalna duljina lozinke je 100 znakova!")]
        public string Lozinka { get; set; }
    }
}