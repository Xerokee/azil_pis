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

                userEntity.ime = userDomain.Ime;
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

        public async Task<IEnumerable<KucniLjubimci>> GetAnimalsByType(string type)
        {
            var animals = await appDbContext.KucniLjubimci
                .Where(a => EF.Functions.Like(a.tip_ljubimca, type)) // Using EF.Functions.Like for better compatibility
                .ToListAsync();

            return animals;
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals()
        {
            return await appDbContext.KucniLjubimci.Where(a => a.udomljen).ToListAsync();
        }

        public async Task<KucniLjubimci> GetKucniLjubimacById(int id)
        {
            return await appDbContext.KucniLjubimci.FindAsync(id);
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

        public async Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption)
        {
            try
            {
                // Add to dnevnik_udomljavanja
                await appDbContext.DnevnikUdomljavanja.AddAsync(adoption);

                // Update kucni_ljubimci
                var animal = await appDbContext.KucniLjubimci.FindAsync(adoption.id_ljubimca);
                if (animal != null)
                {
                    animal.udomljen = true;
                    appDbContext.KucniLjubimci.Update(animal);
                }

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
            var adoption = await appDbContext.DnevnikUdomljavanja.FindAsync(id);
            if (adoption == null)
            {
                return false;
            }

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

                // Ažuriraj samo polje status_udomljavanja u tabeli DnevnikUdomljavanja
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
    }
}