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
        IEnumerable<Aktivnosti> GetAllUsersDb7();
        IEnumerable<Slika> GetAllUsersDb8();
        IEnumerable<SifrTipLjubimca> GetAllUsersDb9();
        IEnumerable<SifrBojaLjubimca> GetAllUsersDb10();
        UsersDomain GetUserDomainByUserId(int id_korisnika);
        Task<UserRoleModel> GetUserRoleById(int id_korisnika);
        UsersDomain GetUserDomainByEmail(string email);
        Task<bool> AddUserAsync(Korisnici userEntity);
        Task<bool> UpdateUserAsync(UsersDomain userDomain);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimals();
        List<KucniLjubimci> GetDogsAndCats();
        List<KucniLjubimci> GetAnimalsByType(int type);
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(int type);
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByColorAndAdoptionStatus(int color);
        Task<KucniLjubimciDomain> GetAnimalById(int id);
        Task<bool> UpdateAnimalAsync(KucniLjubimci animal);
        Task<bool> AdoptAnimalAsync(int id);
        Task<bool> RejectAnimalAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals();
        Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery();
        Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id);
        Task<List<KucniLjubimciDomain>> GetAllAnimalsWithImages();
        Task<IEnumerable<KucniLjubimci>> GetFilteredAnimalsByAgeRange(int tipLjubimca, int? minDob, int? maxDob, int? dob, int boja);
        Task<bool> AddAnimalAsync(AnimalsDomain animalDomain);
        Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> DeleteAdoptionAsync(int id);
        Task<bool> UpdateAdoptionStatus(int idLjubimca, int idUdomljavanja);
        Task<UsersDomain> IsValidUser(int id);
        Task<bool> IsValidAnimal(int id);
        string Test();
        Task<DnevnikUdomljavanja> GetAdoptionById(int id);
        Task<bool> GetAdoptionStatus(int id); 
        Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja);
        Task<DnevnikUdomljavanja> GetAdoptionByUserId(int idKorisnika);
        Task<bool> SetAdoptionStatusByUserId(int idKorisnika, bool status_udomljavanja);
        Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections();
        Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca);
        Task<bool> DeleteRejectionAsync(int id);
        Task<bool> UpdateAnimal(KucniLjubimci animal, int id);
        Task<List<ActivityDomain>> GetAktivnostiById(int id_ljubimca);
        Task<bool> AddAktivnostAsync(Aktivnosti aktivnostRest);
        Task<bool> AddImage(Slika novaSlika);
        Task<List<SlikaDomain>> GetSlikeById(int id_ljubimca);
        Task<bool> DeleteSlikaAsync(int id);
        Task<bool> AdoptAnimalByAdmin(int id);
        Task<IEnumerable<SifrTipLjubimcaDomain>> GetSifrarnik();
        List<KucniLjubimciDomain> GetKucniLjubimci();
        StatistikaDomain GetStatistika();
        Task<IEnumerable<SifrBojaLjubimcaDomain>> GetSifrarnik2();
        Task<string> GetToken(string email);
        Task<List<Meeting>> GetMeetings();
        Task<bool> AddMeeting(Meeting newMeeting);
        Task<bool> DeleteMeeting(int idMeeting);
        Task<bool> EditMeeting(int idMeeting, int idKorisnik, int type);
    }
}