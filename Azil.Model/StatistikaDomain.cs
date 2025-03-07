public class StatistikaDomain
{
    public int raspolozive_zivotinje { get; set; }
    public int udomljene_zivotinje { get; set; }
    public int broj_zivotinja { get; set; }
    public int broj_odbijenih_zahtjeva { get; set; }
    public int broj_zahtjeva { get; set; }

    public StatistikaDomain(int rz, int uz, int bz, int boz, int br, int bv)
    {
        raspolozive_zivotinje = rz;
        udomljene_zivotinje = uz;
        broj_zivotinja = bz;
        broj_odbijenih_zahtjeva = boz;
        broj_zahtjeva = br;
    }

    public StatistikaDomain(int raspolozive_zivotinje, int udomljene_zivotinje, int broj_zivotinja, int broj_odbijenih_zahtjeva, int broj_zahtjeva)
    {
        this.raspolozive_zivotinje = raspolozive_zivotinje;
        this.udomljene_zivotinje = udomljene_zivotinje;
        this.broj_zivotinja = broj_zivotinja;
        this.broj_odbijenih_zahtjeva = broj_odbijenih_zahtjeva;
        this.broj_zahtjeva = broj_zahtjeva;
    }
}