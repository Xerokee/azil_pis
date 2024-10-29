using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public class Slika
    {
        public int id { get; set; }
        public int id_ljubimca { get; set; }
        public byte[] slika_data { get; set; }
    }
}
