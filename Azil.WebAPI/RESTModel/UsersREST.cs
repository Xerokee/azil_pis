namespace Azil.WebAPI.RESTModel
{
    public class UsersREST
    {
        public int? IdKorisnika { get; set; }
        public string? Ime { get; set; }
        public string? Email { get; set; }
        public string? Lozinka { get; set; }
        public bool? Admin { get; set; }
    }
}