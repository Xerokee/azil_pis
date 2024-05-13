using System;
using System.Collections.Generic;

namespace Azil.WebAPI.Models
{
    public partial class KorisnikUloga
    {
        public int IdKorisnika { get; set; }
        public int IdUloge { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }

        public virtual Uloge IdKorisnikaNavigation { get; set; }
    }
}
