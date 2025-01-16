using Azil.DAL.DataModel;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Model
{
    public class KucniLjubimciDomain
    {
        public int id_ljubimca { get; set; }
        public int id_udomitelja { get; set; }
        public string ime_ljubimca { get; set; }
        public string tip_ljubimca { get; set; }
        public string opis_ljubimca { get; set; }
        public bool udomljen { get; set; }
        public bool zahtjev_udomljen { get; set; }
        public string imgUrl { get; set; }
        public List<GalerijaZivotinja> galerijaZivotinja { get; set; }
        public int dob { get; set; }
        public string boja { get; set; }

        public KucniLjubimciDomain(int idlj, int idu, string ime, string tip, string opis, bool u, bool zu, string img, List<GalerijaZivotinja> gz, int d, string b)
        {
            id_ljubimca = idlj;
            id_udomitelja = idu;
            ime_ljubimca = ime;
            tip_ljubimca = tip;
            opis_ljubimca = opis;
            udomljen = u;
            zahtjev_udomljen = zu;
            imgUrl = img;
            galerijaZivotinja = gz;
            dob = d;
            boja = b;
        }
    }
}
