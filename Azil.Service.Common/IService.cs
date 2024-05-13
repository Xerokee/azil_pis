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
        Task<Tuple<UsersDomain, List<ErrorMessage>>> GetUserDomainByUserId(int userId);
        Task<bool> AddUserAsync(UsersDomain userDomain);
        Task<bool> IsValidUser(int id);
    }
}