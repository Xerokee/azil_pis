using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class OdbijeneZivotinje
    {
        public int id { get; set; }
        public int id_korisnika { get; set; }
        public string ime_ljubimca { get; set; }
    }
}
