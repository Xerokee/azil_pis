﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Azil.DAL.DataModel
{
    public partial class KucniLjubimci
    {
        public KucniLjubimci() { }
        public int id_ljubimca { get; set; }
        public int id_udomitelja { get; set; }
        public string ime_ljubimca { get; set; }
        public int tip_ljubimca { get; set; }
        public string opis_ljubimca { get; set; }
        public bool udomljen { get; set; }
        public bool zahtjev_udomljen { get; set; }
        public string imgUrl { get; set; }
        public List<GalerijaZivotinja> galerijaZivotinja { get; set; }
        public int dob { get; set; }
        public int boja { get; set; }
    }
}