namespace Azil.WebAPI.Models
{
    public partial class OdbijeneZivotinje
    {
        public int Id { get; set; }
        public int IdLjubimca { get; set; }
        public int IdKorisnika { get; set; }
        public string ImeLjubimca { get; set; }
        public bool ZahtjevUdomljen { get; set; }
    }
}
