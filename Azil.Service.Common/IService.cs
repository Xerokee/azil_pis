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
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByUserId(int userId);
        Task<bool> AddUserAsync(UsersDomain userDomain);
        Task<bool> IsValidUser(int id);
    }
}