using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Model
{
    public class SlikaDomain
    {
        public int id { get; set; }
        public int id_ljubimca { get; set; }
        public string slika_data { get; set; }
        public SlikaDomain(int i, int ilj, string d)
        {
            id = i;
            id_ljubimca = ilj;
            slika_data = d;
        }
    }
}
