using Azil.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azil.Model
{
    public class UsersDomain
    {
        public UsersDomain()
        {

        }
        public UsersDomain(Korisnici user)
        {
            IdKorisnika = user.id_korisnika;
            Ime = user.ime;
            Email = user.email;
            Lozinka = user.lozinka;
            Admin = user.admin;
        }

        public int IdKorisnika { get; set; }
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite ime korisnika.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime korisnika mora biti između 3 i 50 slova")]
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public bool Admin { get; set; }
    }
}