﻿using Azil.DAL.DataModel;
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
            id_korisnika = user.id_korisnika;
            KorisnickoIme = user.korisnickoIme;
            Ime = user.ime;
            Prezime = user.prezime;
            Email = user.email;
            Lozinka = user.lozinka;
            Admin = user.admin;
            ProfileImg = user.profileImg;
            Token = user.token;
        }

        public int id_korisnika { get; set; }

        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Unesite ime korisnika.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime korisnika mora biti između 3 i 50 slova")]
        public string Ime { get; set; }

        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite email korisnika.")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite lozinku korisnika.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Lozinka mora biti između 6 i 100 znakova.")]
        public string Lozinka { get; set; }
        public bool Admin { get; set; }
        public string ProfileImg { get; set; }
        public string Token { get; set; }
    }
}