using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.Model
{
    public class ActivityDomain
    {
        public int id { get; set; }
        public int id_ljubimca { get; set; }
        public string datum { get; set; }
        public string opis { get; set; }

        public ActivityDomain(int id, int id_ljubimca, DateTime? datum, string opis)
        {
            this.id = id;
            this.id_ljubimca = id_ljubimca;
            this.datum = datum.Value.ToShortDateString();
            this.opis = opis;
        }
    }
}
