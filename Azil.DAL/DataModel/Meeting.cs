using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class Meeting
    {
        public Meeting(int idMeeting, DateTime datum, string vrijeme, int idKorisnik, string imeKorisnik)
        {
            this.idMeeting = idMeeting;
            this.datum = datum;
            this.vrijeme = vrijeme;
            this.idKorisnik = idKorisnik;
            this.imeKorisnik = imeKorisnik;
        }

        public int idMeeting { get; set; }
        public DateTime datum { get; set; }
        public string vrijeme { get; set; }
        public int idKorisnik { get; set; }
        public string imeKorisnik { get; set; }
    }
}
