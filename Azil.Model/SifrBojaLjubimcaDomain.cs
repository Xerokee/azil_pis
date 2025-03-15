using Azil.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Model
{
    public class SifrBojaLjubimcaDomain
    {
        public SifrBojaLjubimcaDomain()
        {

        }

        public SifrBojaLjubimcaDomain(SifrBojaLjubimca sifrarnik2)
        {
            id = sifrarnik2.id;
            naziv = sifrarnik2.naziv;
        }

        public int id { get; set; }
        public string naziv { get; set; }
    }
}
