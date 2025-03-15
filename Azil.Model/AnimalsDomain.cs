using Azil.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azil.Model
{
    public class AnimalsDomain
    {
        public AnimalsDomain()
        {

        }

        public AnimalsDomain(KucniLjubimci animal)
        {
            IdLjubimca = animal.id_ljubimca;
            IdUdomitelja = animal.id_udomitelja;
            ImeLjubimca = animal.ime_ljubimca;
            TipLjubimca = animal.tip_ljubimca;
            OpisLjubimca = animal.opis_ljubimca;
            Udomljen = animal.udomljen;
            ZahtjevUdomljen = animal.zahtjev_udomljen;
            ImgUrl = animal.imgUrl;
            GalerijaZivotinja = animal.galerijaZivotinja;
            Dob = animal.dob;
            Boja = animal.boja;
        }

        public int IdLjubimca { get; set; }
        public int IdUdomitelja { get; set; }

        [Required(ErrorMessage = "Unesite ime ljubimca.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime ljubimca mora biti između 3 i 50 slova")]
        public string ImeLjubimca { get; set; }

        [Required(ErrorMessage = "Odaberite tip ljubimca.")]
        public int TipLjubimca { get; set; }

        [Required(ErrorMessage = "Unesite opis ljubimca.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Opis ljubimca mora biti između 3 i 200 slova")]
        public string OpisLjubimca { get; set; }
        public bool Udomljen { get; set; }
        public bool ZahtjevUdomljen { get; set; } = false;

        public string ImgUrl { get; set; }
        public List<GalerijaZivotinja> GalerijaZivotinja { get; set; }
        public int Dob { get; set; }
        public int Boja { get; set; }
    }
}

