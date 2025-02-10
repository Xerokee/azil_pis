using Microsoft.EntityFrameworkCore.ChangeTracking;
using Azil.DAL.DataModel;
using Azil.Model;
using Azil.Repository.Automapper;
using Azil.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Azil.Repository
{
    public class Repository : IRepository
    {
        private const int MIN_DOB = 0;  // Minimalna dob životinja
        private const int MAX_DOB = 20; // Maksimalna dob životinja

        private readonly Azil_DbContext appDbContext;
        private IRepositoryMappingService _mapper;
        private readonly ILogger<Repository> _logger;

        public Repository(Azil_DbContext appDbContext, IRepositoryMappingService mapper, ILogger<Repository> logger)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public string Test()
        {
            return "I am OK - Repository.";
        }
        public IEnumerable<UsersDomain> GetAllUsers()
        {
            IEnumerable<Korisnici> usersDb = appDbContext.Korisnici.ToList();

            IEnumerable<UsersDomain> usersDomain = _mapper.Map<IEnumerable<UsersDomain>>(usersDb);
            return usersDomain;
        }

        public IEnumerable<Korisnici> GetAllUsersDb()
        {
            IEnumerable<Korisnici> userDb = appDbContext.Korisnici.ToList();
            return userDb;
        }

        public IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2()
        {
            IEnumerable<DnevnikUdomljavanja> userDb2 = appDbContext.DnevnikUdomljavanja.ToList();
            return userDb2;
        }

        public IEnumerable<KorisnikUloga> GetAllUsersDb3()
        {
            IEnumerable<KorisnikUloga> userDb3 = appDbContext.KorisnikUloga.ToList();
            return userDb3;
        }

        public IEnumerable<KucniLjubimci> GetAllUsersDb4()
        {
            IEnumerable<KucniLjubimci> userDb4 = appDbContext.KucniLjubimci.ToList();
            return userDb4;
        }

        public List<KucniLjubimciDomain> GetKucniLjubimci()
        {
            IEnumerable<KucniLjubimci> userDb4 = appDbContext.KucniLjubimci.ToList();
            IEnumerable<SifrTipLjubimca> tipoviLjubimcaDb = appDbContext.Sifrarnik.ToList();

            List<KucniLjubimciDomain> kucniLjubimci = new List<KucniLjubimciDomain>();
            foreach (KucniLjubimci lj in userDb4)
            {
                kucniLjubimci.Add(new KucniLjubimciDomain(lj.id_ljubimca, lj.id_udomitelja, lj.ime_ljubimca, tipoviLjubimcaDb.FirstOrDefault(tip => tip.id == lj.tip_ljubimca).naziv, lj.opis_ljubimca, lj.udomljen, lj.zahtjev_udomljen, lj.imgUrl, lj.galerijaZivotinja, lj.dob, lj.boja));
            }
            return kucniLjubimci;
        }

        public IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5()
        {
            IEnumerable<KucniLjubimciUdomitelj> userDb5 = appDbContext.KucniLjubimciUdomitelj.ToList();
            return userDb5;
        }

        public IEnumerable<Uloge> GetAllUsersDb6()
        {
            IEnumerable<Uloge> userDb6 = appDbContext.Uloge.ToList();
            return userDb6;
        }

        public IEnumerable<Aktivnosti> GetAllUsersDb7()
        {
            IEnumerable<Aktivnosti> userDb7 = appDbContext.Aktivnosti.ToList();
            return userDb7;
        }

        public IEnumerable<Slika> GetAllUsersDb8()
        {
            IEnumerable<Slika> userDb8 = appDbContext.Slike.ToList();
            return userDb8;
        }

        public IEnumerable<SifrTipLjubimca> GetAllUsersDb9()
        {
            IEnumerable<SifrTipLjubimca> userDb9 = appDbContext.Sifrarnik.ToList();
            return userDb9;
        }

        public UsersDomain GetUserDomainByUserId(int id_korisnika)
        {
            Korisnici userDb = appDbContext.Korisnici.Find(id_korisnika);

            UsersDomain user = _mapper.Map<UsersDomain>(userDb);

            return user;
        }

        public async Task<UserRoleModel> GetUserRoleById(int id_korisnika)
        {
            var userRole = await appDbContext.KorisnikUloga
                .Include(ku => ku.Uloge)
                .Where(ku => ku.id_korisnika == id_korisnika)
                .Select(ku => new UserRoleModel
                {
                    IdUloge = ku.id_uloge,
                    NazivUloge = ku.Uloge.naziv_uloge
                })
                .FirstOrDefaultAsync();
            return userRole;
        }

        public UsersDomain GetUserDomainByEmail(string email)
        {
            _logger.LogInformation("Poziv metode GetUserDomainByEmail u Repository s emailom: {Email}", email);
            Korisnici userDb = appDbContext.Korisnici.FirstOrDefault(u => u.email == email);
            if (userDb != null)
            {
                _logger.LogInformation("Korisnik pronađen u bazi podataka: {UserDb}", userDb);
            }
            else
            {
                _logger.LogWarning("Korisnik nije pronađen u bazi podataka: {Email}", email);
            }
            UsersDomain user = _mapper.Map<UsersDomain>(userDb);
            return user;
        }

        public async Task<bool> AddUserAsync(Korisnici userEntity)
        {
            try
            {
                _logger.LogInformation("Adding user to database: {@userEntity}", userEntity);
                await appDbContext.Korisnici.AddAsync(userEntity);
                await appDbContext.SaveChangesAsync();
                _logger.LogInformation("User successfully added to database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user to the database.");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UsersDomain userDomain)
        {
            try
            {
                Korisnici userEntity = await appDbContext.Korisnici.FindAsync(userDomain.id_korisnika);
                if (userEntity == null) return false;

                userEntity.korisnickoIme = userDomain.KorisnickoIme;
                userEntity.ime = userDomain.Ime;
                userEntity.prezime = userDomain.Prezime;
                userEntity.email = userDomain.Email;
                userEntity.lozinka = userDomain.Lozinka;
                userEntity.admin = userDomain.Admin;
                userEntity.profileImg = userDomain.ProfileImg;

                appDbContext.Korisnici.Update(userEntity);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                Korisnici userDb = await appDbContext.Korisnici.FindAsync(id);
                if (userDb != null)
                {
                    appDbContext.Korisnici.Remove(userDb);
                    await appDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAllAnimals()
        {
            return await appDbContext.KucniLjubimci.ToListAsync();
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(int type)
        {
            // Pridruži tabelu `DnevnikUdomljavanja` sa tabelom `KucniLjubimci` kako bi se proverio status udomljavanja
            var animals = await appDbContext.KucniLjubimci
                .Where(a => a.tip_ljubimca == type) // Filtriraj životinje po tipu
                .GroupJoin(
                    appDbContext.DnevnikUdomljavanja,
                    animal => animal.id_ljubimca,
                    adoption => adoption.id_ljubimca,
                    (animal, adoptions) => new { animal, adoptions }
                )
                .SelectMany(
                    joined => joined.adoptions.DefaultIfEmpty(),
                    (joined, adoption) => new { joined.animal, udomljen = adoption != null && adoption.udomljen }
                )
                .Where(result => !result.udomljen) // Filtriraj samo neudomljene životinje
                .Select(result => result.animal)   // Vrati samo informacije o životinjama
                .ToListAsync();

            return animals;
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals()
        {
            return await appDbContext.KucniLjubimci.Where(a => a.udomljen).ToListAsync();
        }

        public async Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery()
        {
            return await appDbContext.GalerijaZivotinja.ToListAsync();
        }

        public async Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id)
        {
            return await appDbContext.GalerijaZivotinja
                .Where(g => g.id_ljubimca == id)
                .ToListAsync();
        }

        public async Task<KucniLjubimciDomain> GetAnimalById(int id)
        {
            IEnumerable<SifrTipLjubimca> tipoviLjubimcaDb = appDbContext.Sifrarnik.ToList();
            var animalDb = await appDbContext.KucniLjubimci
                .Include(a => a.galerijaZivotinja)
                .FirstOrDefaultAsync(a => a.id_ljubimca == id);
            KucniLjubimciDomain animal = new KucniLjubimciDomain(animalDb.id_ljubimca, animalDb.id_udomitelja, animalDb.ime_ljubimca, tipoviLjubimcaDb.FirstOrDefault(tip => tip.id == animalDb.tip_ljubimca).naziv, animalDb.opis_ljubimca, animalDb.udomljen, animalDb.zahtjev_udomljen, animalDb.imgUrl, animalDb.galerijaZivotinja, animalDb.dob, animalDb.boja);
            
            return animal;
        }

        public async Task<bool> UpdateAnimalAsync(KucniLjubimci animal)
        {
            try
            {
                appDbContext.KucniLjubimci.Update(animal);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AdoptAnimalAsync(int id)
        {
            var animal = await appDbContext.KucniLjubimci.FindAsync(id);
            if (animal == null) return false;
            animal.zahtjev_udomljen = true;
            return await UpdateAnimalAsync(animal);
        }

        public async Task<bool> RejectAnimalAsync(int id)
        {
            var animal = await appDbContext.KucniLjubimci.FindAsync(id);
            if (animal == null) return false;
            animal.zahtjev_udomljen = false;
            return await UpdateAnimalAsync(animal);
        }

        public async Task<List<KucniLjubimciDomain>> GetAllAnimalsWithImages()
        {
            var animals = await appDbContext.KucniLjubimci
                .Include(a => a.galerijaZivotinja)
                .ToListAsync();

            IEnumerable<KucniLjubimci> userDb4 = appDbContext.KucniLjubimci.ToList();
            IEnumerable<SifrTipLjubimca> tipoviLjubimcaDb = appDbContext.Sifrarnik.ToList();

            List<KucniLjubimciDomain> kucniLjubimci = new List<KucniLjubimciDomain>();
            foreach (KucniLjubimci lj in userDb4)
            {
                kucniLjubimci.Add(new KucniLjubimciDomain(lj.id_ljubimca, lj.id_udomitelja, lj.ime_ljubimca, tipoviLjubimcaDb.FirstOrDefault(tip => tip.id == lj.tip_ljubimca).naziv, lj.opis_ljubimca, lj.udomljen, lj.zahtjev_udomljen, lj.imgUrl, lj.galerijaZivotinja, lj.dob, lj.boja));
            }
            return kucniLjubimci;
        }

        public async Task<bool> AddAnimalAsync(AnimalsDomain animalDomain)
        {
            try
            {
                var animalEntity = _mapper.Map<KucniLjubimci>(animalDomain);
                _logger.LogInformation("Adding animal to database: {@animalEntity}", animalEntity);
                await appDbContext.KucniLjubimci.AddAsync(animalEntity);
                await appDbContext.SaveChangesAsync();
                _logger.LogInformation("Animal successfully added to database");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding animal to the database.");
                return false;
            }
        }


        public async Task<IEnumerable<KucniLjubimci>> GetFilteredAnimalsByAgeRange(int tipLjubimca, int? minDob, int? maxDob, int? dob, string boja)
        {
            var query = appDbContext.KucniLjubimci.AsQueryable();

            // Filtriraj po tipu ljubimca ako tipLjubimca nije 0 (ili neodređen)
            if (tipLjubimca > 0)
            {
                // Poveži tip ljubimca sa nazivom iz šifrarnika
                var tipLjubimcaNaziv = await appDbContext.Sifrarnik
                                                         .Where(s => s.id == tipLjubimca)
                                                         .Select(s => s.naziv)
                                                         .FirstOrDefaultAsync();

                if (tipLjubimcaNaziv != null)
                {
                    // Ako je tip ljubimca pronađen, filtriraj po njemu
                    query = query.Where(a => a.tip_ljubimca == tipLjubimca);
                }
            }

            // Ako je specificirana konkretna dob, koristimo je za filtriranje
            if (dob.HasValue)
            {
                query = query.Where(a => a.dob == dob.Value);
            }
            else if (minDob.HasValue && maxDob.HasValue)
            {
                // Ako su zadani minDob i maxDob, filtriraj prema njima
                query = query.Where(a => a.dob >= minDob && a.dob <= maxDob);
            }
            else if (minDob.HasValue)
            {
                // Ako je zadana samo minimalna dob, filtriraj sve životinje s dobi većom ili jednakom minDob
                query = query.Where(a => a.dob >= minDob);
            }
            else if (maxDob.HasValue)
            {
                // Ako je zadana samo maksimalna dob, filtriraj sve životinje s dobi manjom ili jednakom maxDob
                query = query.Where(a => a.dob <= maxDob);
            }

            // Logika za specifične dobne raspone:
            if (minDob == 0 && maxDob == 1)
            {
                // Raspodijeli u dobni raspon od 0 do 1 godinu
                query = query.Where(a => a.dob >= 0 && a.dob <= 1);
            }
            else if (minDob == 2 && maxDob == 5)
            {
                // Raspodijeli u dobni raspon od 2 do 5 godina
                query = query.Where(a => a.dob >= 2 && a.dob <= 5);
            }
            else if (minDob >= 5)
            {
                // Raspodijeli sve životinje koje imaju više od 5 godina
                query = query.Where(a => a.dob >= 5);
            }

            // Filtriraj po boji
            if (!string.IsNullOrEmpty(boja) && boja != "Sve")
            {
                query = query.Where(a => a.boja == boja);
            }

            // Filtriraj da se prikažu samo životinje koje nisu udomljene
            query = query.Where(a => !a.udomljen && !a.zahtjev_udomljen);

            return await query.ToListAsync();
        }


        public async Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption)
        {
            try
            {
                // Add to dnevnik_udomljavanja
                await appDbContext.DnevnikUdomljavanja.AddAsync(adoption);
                // EntityEntry<DnevnikUdomljavanja> dnevnik_created = await appDbContext.DnevnikUdomljavanja.AddAsync(adoption);

                // Update kucni_ljubimci

                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding adoption.");
                return false;
            }
        }

        public async Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption)
        {
            var existingAdoption = await appDbContext.DnevnikUdomljavanja.FindAsync(adoption.id_ljubimca);
            if (existingAdoption == null)
            {
                return false;
            }

            // existingAdoption.id_ljubimca = adoption.id_ljubimca;
            existingAdoption.id_korisnika = adoption.id_korisnika;
            existingAdoption.ime_ljubimca = adoption.ime_ljubimca;
            existingAdoption.tip_ljubimca = adoption.tip_ljubimca;
            existingAdoption.udomljen = adoption.udomljen;
            existingAdoption.datum = adoption.datum;
            existingAdoption.vrijeme = adoption.vrijeme;
            existingAdoption.imgUrl = adoption.imgUrl;
            existingAdoption.stanje_zivotinje = adoption.stanje_zivotinje;
            existingAdoption.status_udomljavanja = adoption.status_udomljavanja;

            appDbContext.DnevnikUdomljavanja.Update(existingAdoption);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAdoptionAsync(int id)
        {
            try
            {
                // Prvo provjeri postoji li zapis
                var adoption = await appDbContext.DnevnikUdomljavanja
                    .Where(a => a.id == id)
                    .FirstOrDefaultAsync();

                if (adoption == null)
                {
                    Console.WriteLine($"DeleteAdoptionAsync: Ne postoji zapis s id={id}");
                    return false;
                }

                // Ako postoji, obriši ga
                appDbContext.DnevnikUdomljavanja.Remove(adoption);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom brisanja udomljavanja: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateAdoptionStatus(int idLjubimca, int idUdomljavanja)
        {
            var kucniLjubimac = await appDbContext.KucniLjubimci.FindAsync(idLjubimca);
            kucniLjubimac.zahtjev_udomljen = false;
            kucniLjubimac.udomljen = false;
            kucniLjubimac.id_udomitelja = 0;

            appDbContext.KucniLjubimci.Update(kucniLjubimac);

            var adoption = await appDbContext.DnevnikUdomljavanja.FirstOrDefaultAsync(a => a.id_ljubimca == idUdomljavanja);
            appDbContext.DnevnikUdomljavanja.Remove(adoption);

            await appDbContext.SaveChangesAsync();
            return true;
        }


        //public async Task<bool> UpdateUserOibAsync(UsersDomain userDomain)
        //{
        //	try
        //	{
        //		PisUsersMmargeta userDb =await appDbContext.PisUsersMmmargeta.Find(userDomain.UserId);

        //		if(userDb == null)
        //		{
        //		}
        //			return true;
        //	}
        //	catch(Exception ex)
        //	{
        //		return false;
        //	}
        //}

        public async Task<UsersDomain> IsValidUser(int id)
        {
            Korisnici userDb = await appDbContext.Korisnici.FindAsync(id);
            return _mapper.Map<UsersDomain>(userDb);
        }

        public async Task<bool> IsValidAnimal(int id)
        {
            KucniLjubimci animalDb = await appDbContext.KucniLjubimci.FindAsync(id);
            return animalDb != null;
        }

        public async Task<DnevnikUdomljavanja> GetAdoptionById(int id)
        {
            return await appDbContext.DnevnikUdomljavanja.FindAsync(id);
        }

        public async Task<bool> GetAdoptionStatus(int id)
        {
            var adoption = await appDbContext.DnevnikUdomljavanja.FindAsync(id);
            return adoption?.status_udomljavanja ?? false; // Vraćamo false ako nije pronađeno
        }

        public async Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja)
        {
            try
            {
                var adoption = await appDbContext.DnevnikUdomljavanja.FindAsync(idLjubimca);
                if (adoption == null) return false;

                adoption.udomljen = false;
                adoption.id_korisnika = 0;

                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating adoption status.");
                return false;
            }
        }

        public async Task<DnevnikUdomljavanja> GetAdoptionByUserId(int idKorisnika)
        {
            // Dohvaćanje zapisa udomljavanja prema ID-u korisnika
            return await appDbContext.DnevnikUdomljavanja
                .FirstOrDefaultAsync(adoption => adoption.id_korisnika == idKorisnika);
        }

        public async Task<bool> SetAdoptionStatusByUserId(int idKorisnika, bool status_udomljavanja)
        {
            try
            {
                // Dohvati zapis udomljavanja prema idKorisnika
                var adoption = await appDbContext.DnevnikUdomljavanja
                    .FirstOrDefaultAsync(adoption => adoption.id_korisnika == idKorisnika);

                if (adoption == null)
                {
                    return false;
                }

                // Ažuriraj status udomljavanja
                adoption.status_udomljavanja = status_udomljavanja;
                appDbContext.Entry(adoption).Property(a => a.status_udomljavanja).IsModified = true;

                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating adoption status.");
                return false;
            }
        }

        public async Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections()
        {
            return await appDbContext.OdbijeneZivotinje.ToListAsync();
        }

        public async Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca)
        {
            try
            {
                // Dohvati životinju iz dnevnik_udomljavanja prema danom imenu ljubimca
                var animalInAdoption = await appDbContext.DnevnikUdomljavanja
                    .FirstOrDefaultAsync(a => a.ime_ljubimca == imeLjubimca);

                if (animalInAdoption == null)
                {
                    _logger.LogWarning($"Životinja s imenom {imeLjubimca} nije pronađena u dnevnik_udomljavanja.");
                    return false;  // Životinja nije pronađena
                }

                // Ako je životinja pronađena, koristi ispravno ime_ljubimca iz dnevnik_udomljavanja
                var rejection = new OdbijeneZivotinje
                {
                    id_korisnika = userId,
                    id_ljubimca = animalId,
                    ime_ljubimca = animalInAdoption.ime_ljubimca  // Koristi ime ljubimca
                };

                await appDbContext.OdbijeneZivotinje.AddAsync(rejection);
                await appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška pri spremanju odbijanja u bazu podataka.");
                return false;
            }
        }


        public async Task<bool> DeleteRejectionAsync(int id)
        {
            try
            {
                var rejection = await appDbContext.OdbijeneZivotinje.FindAsync(id);
                if (rejection == null) return false;

                appDbContext.OdbijeneZivotinje.Remove(rejection);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting rejection.");
                return false;
            }
        }

        public async Task<bool> UpdateAnimal(KucniLjubimci animal, int id)
        {
            try
            {
                KucniLjubimci animalDb = await appDbContext.KucniLjubimci.FindAsync(id);
                if (animalDb != null)
                {
                    animalDb.ime_ljubimca = animal.ime_ljubimca;
                    animalDb.dob = animal.dob;
                    animalDb.opis_ljubimca = animal.opis_ljubimca;
                }
                appDbContext.KucniLjubimci.Update(animalDb);

                var dnevnikDb = await appDbContext.DnevnikUdomljavanja.Where(d => d.id == id).ToListAsync();

                if (dnevnikDb.Any())
                {
                    foreach (var d in dnevnikDb)
                    {
                        d.ime_ljubimca = animal.ime_ljubimca;
                        appDbContext.DnevnikUdomljavanja.Update(d);
                    }
                }


                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ActivityDomain>> GetAktivnostiById(int id_ljubimca)
        {
            IEnumerable<Aktivnosti> aktivnostiDb = await appDbContext.Aktivnosti.Where(a => a.id_ljubimca == id_ljubimca).ToListAsync();
            aktivnostiDb = aktivnostiDb.OrderByDescending(a => a.datum).ToList();
            List<ActivityDomain> aktivnostiDomain = new List<ActivityDomain>();
            foreach(Aktivnosti a in aktivnostiDb)
            {
                aktivnostiDomain.Add(new ActivityDomain(a.id, a.id_ljubimca, a.datum, a.aktivnost, a.opis));
            }
            return aktivnostiDomain;
        }

        public async Task<bool> AddAktivnostAsync(Aktivnosti aktivnostRest)
        {
            try
            {
                EntityEntry<Aktivnosti> aktivnost_created = await appDbContext.Aktivnosti.AddAsync(aktivnostRest);

                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding activity.");
                return false;
            }
        }

        public async Task<bool> AddImage(Slika novaSlika)
        {
            try
            {

                await appDbContext.Slike.AddAsync(novaSlika);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding image.");
                return false;
            }
        }

        public async Task<List<SlikaDomain>> GetSlikeById(int id_ljubimca)
        {
            IEnumerable<Slika> slikeDb = await appDbContext.Slike.Where(s => s.id_ljubimca == id_ljubimca).ToListAsync();
            List<SlikaDomain> slikeDomain = new List<SlikaDomain>();
            foreach (Slika s in slikeDb)
            {
                slikeDomain.Add(new SlikaDomain(s.id, s.id_ljubimca, Convert.ToBase64String(s.slika_data)));
            }
            return slikeDomain;
        }

        public async Task<bool> DeleteSlikaAsync(int id)
        {
            try
            {
                Slika slikaDb = await appDbContext.Slike.FindAsync(id);
                if (slikaDb != null)
                {
                    appDbContext.Slike.Remove(slikaDb);
                    await appDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AdoptAnimalByAdmin(int id)
        {
            try
            {
                KucniLjubimci animal = await appDbContext.KucniLjubimci.FindAsync(id);
                animal.udomljen = true;
                appDbContext.KucniLjubimci.Update(animal);
                await appDbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<SifrTipLjubimcaDomain>> GetSifrarnik()
        {
            IEnumerable<SifrTipLjubimca> tipoviLjubimcaDb = await appDbContext.Sifrarnik.ToListAsync();
            IEnumerable<SifrTipLjubimcaDomain> tipoviLjubimca = _mapper.Map<IEnumerable<SifrTipLjubimcaDomain>>(tipoviLjubimcaDb);
            return tipoviLjubimca;
        }
    }
}