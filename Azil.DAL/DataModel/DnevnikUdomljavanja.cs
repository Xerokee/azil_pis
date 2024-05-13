using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Azil.DAL.DataModel
{
    public partial class DnevnikUdomljavanja
    {
        public int id_ljubimca { get; set; }
        public int id_korisnika { get; set; }
        public DateTime datum { get; set; }
        public string opis { get; set; }
    }
}