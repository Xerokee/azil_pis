using Azil.DAL.DataModel;
using Azil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azil.Repository.Common
{
    public interface IRepository
    {
        IEnumerable<UsersDomain> GetAllUsers();
        IEnumerable<Korisnici> GetAllUsersDb();
        IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2();
        IEnumerable<KorisnikUloga> GetAllUsersDb3();
        IEnumerable<KucniLjubimci> GetAllUsersDb4();
        IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5();
        IEnumerable<Uloge> GetAllUsersDb6();
        UsersDomain GetUserDomainByUserId(int id_korisnika);
        Task<UserRoleModel> GetUserRoleById(int id_korisnika);
        UsersDomain GetUserDomainByEmail(string email);
        Task<bool> AddUserAsync(Korisnici userEntity);
        Task<bool> UpdateUserAsync(UsersDomain userDomain);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimals();
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(string type);
        Task<KucniLjubimci> GetAnimalById(int id);
        Task<bool> UpdateAnimalAsync(KucniLjubimci animal);
        Task<bool> AdoptAnimalAsync(int id);
        Task<bool> RejectAnimalAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals();
        Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery();
        Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimalsWithImages();
        Task<bool> AddAnimalAsync(AnimalsDomain animalDomain);
        Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> DeleteAdoptionAsync(int id);
        Task<UsersDomain> IsValidUser(int id);
        Task<bool> IsValidAnimal(int id);
        string Test();
        Task<DnevnikUdomljavanja> GetAdoptionById(int id);
        Task<bool> GetAdoptionStatus(int id); 
        Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja);
        Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections();
        Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca);
        Task<bool> DeleteRejectionAsync(int id);
    }
}