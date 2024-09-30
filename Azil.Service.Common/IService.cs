using Azil.Common;
using Azil.Model;
using Azil.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azil.Service.Common
{
    public interface IService
    {
        string Test();
        Task<Tuple<IEnumerable<UsersDomain>, List<ErrorMessage>>> GetAllUsers();
        IEnumerable<Korisnici> GetAllUsersDb();
        IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2();
        IEnumerable<KorisnikUloga> GetAllUsersDb3();
        IEnumerable<KucniLjubimci> GetAllUsersDb4();
        IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5();
        IEnumerable<Uloge> GetAllUsersDb6();
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByUserId(int id_korisnika);
        Task<UserRoleModel> GetUserRoleById(int id_korisnika);
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByEmail(string email);
        Task<bool> AddUserAsync(UsersDomain userDomain);
        Task<bool> UpdateUserAsync(UsersDomain userDomain);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimals();
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(string type);
        Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals();
        Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery();
        Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id);
        Task<KucniLjubimci> GetAnimalById(int id);
        Task<bool> UpdateAnimalAsync(KucniLjubimci animal);
        Task<bool> AdoptAnimalAsync(int id);
        Task<bool> RejectAnimalAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimalsWithImages();
        Task<bool> IsValidUser(int id);
        Task<bool> IsValidAnimal(int id);
        Task<bool> AddAnimalAsync(AnimalsDomain animalDomain);
        Task<IEnumerable<KucniLjubimci>> GetFilteredAnimalsByAgeRange(string tipLjubimca, int? dobMin, int? dobMax, string boja);
        Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> DeleteAdoptionAsync(int id);
        Task<DnevnikUdomljavanja> GetAdoptionById(int id);
        Task<bool> GetAdoptionStatus(int id);
        Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja);
        Task<DnevnikUdomljavanja> GetAdoptionStatusByUserId(int idKorisnika);
        Task<bool> SetAdoptionStatusByUserId(int idKorisnika, bool status_udomljavanja);
        Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections();
        Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca);
        Task<bool> DeleteRejectionAsync(int id);
    }
}