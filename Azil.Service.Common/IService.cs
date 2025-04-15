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
        IEnumerable<Aktivnosti> GetAllUsersDb7();
        IEnumerable<Slika> GetAllUsersDb8();
        IEnumerable<SifrTipLjubimca> GetAllUsersDb9();
        IEnumerable<SifrBojaLjubimca> GetAllUsersDb10();
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByUserId(int id_korisnika);
        Task<UserRoleModel> GetUserRoleById(int id_korisnika);
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByEmail(string email);
        Task<bool> AddUserAsync(UsersDomain userDomain);
        Task<bool> UpdateUserAsync(UsersDomain userDomain);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<KucniLjubimci>> GetAllAnimals();
        List<KucniLjubimci> GetDogsAndCats();
        List<KucniLjubimci> GetAnimalsByType(int type);
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByTypeAndAdoptionStatus(int type);
        Task<IEnumerable<KucniLjubimci>> GetAnimalsByColorAndAdoptionStatus(int color);
        Task<IEnumerable<KucniLjubimci>> GetAdoptedAnimals();
        Task<IEnumerable<GalerijaZivotinja>> GetAllAnimalGallery();
        Task<IEnumerable<GalerijaZivotinja>> GetGalleryByAnimalId(int id);
        Task<KucniLjubimciDomain> GetAnimalById(int id);
        Task<bool> UpdateAnimalAsync(KucniLjubimci animal);
        Task<bool> AdoptAnimalAsync(int id);
        Task<bool> RejectAnimalAsync(int id);
        Task<List<KucniLjubimciDomain>> GetAllAnimalsWithImages();
        Task<bool> IsValidUser(int id);
        Task<bool> IsValidAnimal(int id);
        Task<bool> AddAnimalAsync(AnimalsDomain animalDomain);
        Task<IEnumerable<KucniLjubimci>> GetFilteredAnimalsByAgeRange(int tipLjubimca, int? minDob, int? maxDob, int? dob, int boja);
        Task<bool> AddAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> UpdateAdoptionAsync(DnevnikUdomljavanja adoption);
        Task<bool> DeleteAdoptionAsync(int id);
        Task<bool> UpdateAdoptionStatus(int idLjubimca, int idUdomljavanja);
        Task<DnevnikUdomljavanja> GetAdoptionById(int id);
        Task<bool> GetAdoptionStatus(int id);
        Task<bool> SetAdoptionStatus(int idLjubimca, bool status_udomljavanja);
        Task<DnevnikUdomljavanja> GetAdoptionStatusByUserId(int idKorisnika);
        Task<bool> SetAdoptionStatusByUserId(int idKorisnika, bool status_udomljavanja);
        Task<IEnumerable<OdbijeneZivotinje>> GetAllRejections();
        Task<bool> SaveRejectionAsync(int userId, int animalId, string imeLjubimca);
        Task<bool> DeleteRejectionAsync(int id);
        Task<bool> UpdateAnimal(KucniLjubimci animal, int id);
        Task<Tuple<List<ActivityDomain>, List<ErrorMessage>>> GetAktivnostiById(int id_ljubimca);
        Task<bool> AddAktivnostAsync(Aktivnosti aktivnostRest);
        Task<bool> AddImage(Slika novaSlika);
        Task<Tuple<List<SlikaDomain>, List<ErrorMessage>>> GetSlikeById(int id_ljubimca);
        Task<bool> DeleteSlikaAsync(int id);
        Task<bool> AdoptAnimalByAdmin(int id);
        Task<IEnumerable<SifrTipLjubimcaDomain>> GetSifrarnik();
        Task<IEnumerable<SifrBojaLjubimcaDomain>> GetSifrarnik2();
        List<KucniLjubimciDomain> GetKucniLjubimci();
        Task<int?> GetTipLjubimcaId(string tipLjubimca);
        Task<int?> GetBojaLjubimcaId(string bojaLjubimca);
        Tuple<StatistikaDomain, List<ErrorMessage>> GetStatistika();
        Task<string> GetToken(string email);
        Task<Tuple<List<Meeting>, List<ErrorMessage>>> GetMeetings();
        Task<bool> AddMeeting(Meeting newMeeting);
        Task<bool> DeleteMeeting(int idMeeting);
        Task<bool> EditMeeting(int idMeeting, int idKorisnik, int type);
    }
}