using Azil.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Model
{
    public class SifrTipLjubimcaDomain
    {
        public SifrTipLjubimcaDomain()
        {

        }

        public SifrTipLjubimcaDomain(SifrTipLjubimca sifrarnik)
        {
            id = sifrarnik.id;
            naziv = sifrarnik.naziv;
        }

        public int id { get; set; }
        public string naziv { get; set; }
    }
}
