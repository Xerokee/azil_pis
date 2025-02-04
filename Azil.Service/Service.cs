using AutoMapper;
using Azil.Common;
using Azil.DAL.DataModel;
using Azil.Model;
using Azil.Repository.Automapper;
using Azil.Repository.Common;
using Azil.Service.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azil.Service
{
    public class Service : IService
    {
        private readonly Azil_DbContext appDbContext;
        IRepository _repository;
        IRepositoryMappingService _mapper;
        private readonly ILogger<Service> _logger;

        public Service(Azil_DbContext appDbContext, IRepository repository, IRepositoryMappingService mapper, ILogger<Service> logger)
        {
            this.appDbContext = appDbContext;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public string Test()
        {
            return _repository.Test();
        }
        //public IEnumerable<UsersDomain> GetAllUsers()
        //{
        //	List<UsersDomain> users = _repository.GetAllUsers().ToList();

        //	if(users == null)
        //	{

        //		throw new Exception("no users found");
        //	}
        //	else
        //	{ 
        //		return users;

        //	}
        //}

        public IEnumerable<Korisnici> GetAllUsersDb()
        {
            IEnumerable<Korisnici> userDb = _repository.GetAllUsersDb();
            return userDb;
        }

        public IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2()
        {
            IEnumerable<DnevnikUdomljavanja> userDb2 = _repository.GetAllUsersDb2();
            return userDb2;
        }

        public IEnumerable<KorisnikUloga> GetAllUsersDb3()
        {
            IEnumerable<KorisnikUloga> userDb3 = _repository.GetAllUsersDb3();
            return userDb3;
        }

        public IEnumerable<KucniLjubimci> GetAllUsersDb4()
        {
            IEnumerable<KucniLjubimci> userDb4 = _repository.GetAllUsersDb4();
            return userDb4;
        }

        public List<KucniLjubimciDomain> GetKucniLjubimci()
        {
            List<KucniLjubimciDomain> kucniLjubimci = _repository.GetKucniLjubimci();
            return kucniLjubimci;
        }

        public IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5()
        {
            IEnumerable<KucniLjubimciUdomitelj> userDb5 = _repository.GetAllUsersDb5();
            return userDb5;
        }

        public IEnumerable<Uloge> GetAllUsersDb6()
        {
            IEnumerable<Uloge> userDb6 = _repository.GetAllUsersDb6();
            return userDb6;
        }

        public IEnumerable<Aktivnosti> GetAllUsersDb7()
        {
            IEnumerable<Aktivnosti> userDb7 = _repository.GetAllUsersDb7();
            return userDb7;
        }

        public IEnumerable<Slika> GetAllUsersDb8()
        {
            IEnumerable<Slika> userDb8 = _repository.GetAllUsersDb8();
            return userDb8;
        }

        public IEnumerable<SifrTipLjubimca> GetAllUsersDb9()
        {
            IEnumerable<SifrTipLjubimca> userDb9 = _repository.GetAllUsersDb9();
            return userDb9;
        }

        public async Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByUserId(int id_korisnika)
        {
            //return _repository.GetUserDomainByUserId(userId);
            List<ErrorMessage> erorMessages = new List<ErrorMessage>();
            UsersDomain usersDomain = _repository.GetUserDomainByUserId(id_korisnika);


            if (usersDomain != null)
            {
                erorMessages.Add(new ErrorMessage("Podatci su uredu!"));
                erorMessages.Add(new ErrorMessage("Podatci su u ispravnom obliku!"));
            }
            else
            {
                erorMessages.Add(new ErrorMessage("Podatci nisu uredu!"));
                erorMessages.Add(new ErrorMessage("Podatci nisu u ispravnom obliku!"));
            }
            return new Tuple<UsersDomain, List<ErrorMessage>>(usersDomain, erorMessages);
        }

        public async Task<UserRoleModel> GetUserRoleById(int id_korisnika)
        {
            return await _repository.GetUserRoleById(id_korisnika);
        }

        public async Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByEmail(string email)
        {
            _logger.LogInformation("Poziv metode GetUserDomainByEmail s emailom: {Email}", email);
            List<ErrorMessage> errorMessages = new List<ErrorMessage>();
            UsersDomain usersDomain = _repository.GetUserDomainByEmail(email);

            if (usersDomain != null)
            {
                _logger.LogInformation("Korisnik pronađen: {User}", usersDomain);
                errorMessages.Add(new ErrorMessage("Podatci su uredu!"));
                errorMessages.Add(new ErrorMessage("Podatci su u ispravnom obliku!"));
            }
            else
            {
                _logger.LogWarning("Korisnik nije pronađen: {Email}", email);
                errorMessages.Add(new ErrorMessage("Podatci nisu uredu!"));
                errorMessages.Add(new ErrorMessage("Podatci nisu u ispravnom obliku!"));
            }
            return new Tuple<UsersDomain, List<ErrorMessage>>(usersDomain, errorMessages);
        }


        public async Task<bool> AddUserAsync(UsersDomain userDomain)
        {
            _logger.LogInformation("Mapping UsersDomain to Korisnici");
            try
            {
                var userEntity = _mapper.Map<Korisnici>(userDomain);
                _logger.LogInformation("Mapped UsersDomain to Korisnici: {@userEntity}", userEntity);
                bool result = await _repository.AddUserAsync(userEntity);
                _logger.LogInformation("AddUserAsync result: {result}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UsersDomain userDomain)
        {
            var userEntity = _mapper.Map<Korisnici>(userDomain);
            return await _repository.UpdateUserAsync(userDomain);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _repository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAllAnimals()
        {
            return await _repository.GetAllAnimals();
        }
        public async Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(int type)
        {
            // Pozovi repozitorijum da dohvati životinje po tipu i proveri njihov status udomljavanja
            var animals = await _repository.GetAnimalsByTypeAndAdoptionStatus(type);
            return animals;
        }

        public async Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals()
        {
            return await _repository.GetAdoptedAnimals();
        }

        public async Task<bool> UpdateAnimalAsync(KucniLjubimci animal)
        {
            try
            {
                return await _repository.UpdateAnimalAsync(animal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the animal.");
                return false;
            }
        }

        public async Task<bool> AdoptAnimalAsync(int id)
        {
            return await _repository.AdoptAnimalAsync(id);
        }

        public async Task<bool> RejectAnimalAsync(int id)
        {
            return await _repository.RejectAnimalAsync(id);
        }

        public async Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery()
        {
            return await _repository.GetAllAnimalGallery();
        }

        public async Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id)
        {
            return await _repository.GetGalleryByAnimalId(id);
        }

        public async Task<KucniLjubimciDomain> GetAnimalById(int id)
        {
            return await _repository.GetAnimalById(id);
        }

        public async Task<List<KucniLjubimciDomain>> GetAllAnimalsWithImages()
        {
            return await _repository.GetAllAnimalsWithImages();
        }

        public async Task<bool> AddAnimalAsync(AnimalsDomain animalDomain)
        {
            _logger.LogInformation("Početak dodavanja životinje u bazu: {@AnimalDomain}", animalDomain);

            try
            {
                if (!await appDbContext.Sifrarnik.AnyAsync(x => x.id == animalDomain.TipLjubimca))
                {
                    _logger.LogWarning("Tip ljubimca sa ID-jem {TipLjubimca} nije pronađen.", animalDomain.TipLjubimca);
                    return false; // Tip ljubimca nije validan
                }

                var animalEntity = _mapper.Map<KucniLjubimci>(animalDomain);
                _logger.LogInformation("Mapa domain modela na entitet: {@AnimalEntity}", animalEntity);

                await appDbContext.KucniLjubimci.AddAsync(animalEntity);
                await appDbContext.SaveChangesAsync();

                _logger.LogInformation("Životinja uspešno dodana u bazu: {@AnimalEntity}", animalEntity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška pri dodavanju životinje u bazu.");
                return false;
            }
        }

        public async Task<IEnumerable<KucniLjubimci>> GetFilteredAnimalsByAgeRange(int tipLjubimca, int? minDob, int? maxDob, int? dob, string boja)
        {
            return await _repository.GetFilteredAnimalsByAgeRange(tipLjubimca, minDob, maxDob, dob, boja);
        }

        public async Task<int?> GetTipLjubimcaId(string naziv)
        {
            return await appDbContext.Sifrarnik
                .Where(s => s.naziv.ToLower() == naziv.ToLower())
                .Select(s => (int?)s.id)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption)
        {
            try
            {
                await _repository.AddAdoptionAsync(adoption);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding adoption");
                return false;
            }
        }

        public async Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption)
        {
            return await _repository.UpdateAdoptionAsync(adoption);
        }

        public async Task<bool> DeleteAdoptionAsync(int id)
        {
            return await _repository.DeleteAdoptionAsync(id);
        }

        public async Task<bool> UpdateAdoptionStatus(int idLjubimca, int idUdomljavanja)
        {
            return await _repository.UpdateAdoptionStatus(idLjubimca, idUdomljavanja);
        }

        public async Task<DnevnikUdomljavanja> GetAdoptionById(int id)
        {
            return await _repository.GetAdoptionById(id);
        }

        public async Task<bool> GetAdoptionStatus(int id)
        {
            return await _repository.GetAdoptionStatus(id);
        }

        public async Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja)
        {
            try
            {
                return await _repository.SetAdoptionStatus(idLjubimca, status_udomljavanja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting adoption status.");
                return false;
            }
        }

        public async Task<DnevnikUdomljavanja> GetAdoptionStatusByUserId(int idKorisnika)
        {
            return await _repository.GetAdoptionByUserId(idKorisnika);
        }

        public async Task<bool> SetAdoptionStatusByUserId(int idKorisnika, bool status_udomljavanja)
        {
            try
            {
                var adoption = await _repository.GetAdoptionByUserId(idKorisnika);
                if (adoption == null) return false;

                adoption.status_udomljavanja = status_udomljavanja;
                return await _repository.UpdateAdoptionAsync(adoption);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting adoption status by user ID.");
                return false;
            }
        }

        public async Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections()
        {
            return await _repository.GetAllRejections();
        }

        public async Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca)
        {
            return await _repository.SaveRejectionAsync(userId, animalId, imeLjubimca);
        }

        public async Task<bool> DeleteRejectionAsync(int id)
        {
            return await _repository.DeleteRejectionAsync(id);
        }

        public async Task<bool> UpdateAnimal(KucniLjubimci animal, int id)
        {
            try
            {
                return await _repository.UpdateAnimal(animal, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating the animal.");
                return false;
            }
        }

        public async Task<Tuple<List<ActivityDomain>, List<ErrorMessage>>> GetAktivnostiById(int id_ljubimca)
        {
            List<ErrorMessage> erorMessages = new List<ErrorMessage>();
            List<ActivityDomain> aktivnostiDomain = await _repository.GetAktivnostiById(id_ljubimca);


            if (aktivnostiDomain.Count() == 0)
            {
                erorMessages.Add(new ErrorMessage("Nema dodanih aktivnosti!"));
            }
            else
            {
                erorMessages.Add(new ErrorMessage("Dohvaćanje uspješno!"));
            }
            return new Tuple<List<ActivityDomain>, List<ErrorMessage>>(aktivnostiDomain, erorMessages);
        }

        #region AdditionalCustomFunctions

        public async Task<bool> IsValidUser(int id)
        {
            UsersDomain user = await _repository.IsValidUser(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> IsValidAnimal(int id)
        {
            return await _repository.IsValidAnimal(id);
        }

        public async Task<Tuple<IEnumerable<UsersDomain>, List<ErrorMessage>>> GetAllUsers()
        {
            List<ErrorMessage> erorMessages = new List<ErrorMessage>();
            IEnumerable<UsersDomain> usersDomain = _repository.GetAllUsers();
            if (usersDomain != null)
            {
                erorMessages.Add(new ErrorMessage("Podatci su uredu!"));
                erorMessages.Add(new ErrorMessage("Podatci su u ispravnom obliku!"));
            }
            else
            {
                erorMessages.Add(new ErrorMessage("Podatci nisu uredu!"));
                erorMessages.Add(new ErrorMessage("Podatci nisu u ispravnom obliku!"));
            }
            return new Tuple<IEnumerable<UsersDomain>, List<ErrorMessage>>(usersDomain, erorMessages);
        }

        public async Task<bool> AddAktivnostAsync(Aktivnosti aktivnostRest)
        {
            try
            {
                return await _repository.AddAktivnostAsync(aktivnostRest);
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
                await _repository.AddImage(novaSlika);
                return true; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding image.");
                return false;
            }
        }

        public async Task<Tuple<List<SlikaDomain>, List<ErrorMessage>>> GetSlikeById(int id_ljubimca)
        {
            List<ErrorMessage> erorMessages = new List<ErrorMessage>();
            List<SlikaDomain> slikeDomain = await _repository.GetSlikeById(id_ljubimca);


            if (slikeDomain.Count() == 0)
            {
                erorMessages.Add(new ErrorMessage("Nema dodanih slika!"));
            }
            else
            {
                erorMessages.Add(new ErrorMessage("Dohvaćanje uspješno!"));
            }
            return new Tuple<List<SlikaDomain>, List<ErrorMessage>>(slikeDomain, erorMessages);
        }

        public async Task<bool> DeleteSlikaAsync(int id)
        {
            try
            {
                await _repository.DeleteSlikaAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting image.");
                return false;
            }
        }

        public async Task<bool> AdoptAnimalByAdmin(int id)
        {
            try
            {
                return await _repository.AdoptAnimalByAdmin(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adopting animal by admin.");
                return false;
            }
        }

        public async Task<IEnumerable<SifrTipLjubimcaDomain>> GetSifrarnik()
        {
            return await _repository.GetSifrarnik();
        }

        #endregion AdditionalCustomFunctions
    }
}