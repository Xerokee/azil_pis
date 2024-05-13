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
        UsersDomain GetUserDomainByUserId(int userId);
        Task<bool> AddUserAsync(UsersDomain userDomain);
        Task<UsersDomain> IsValidUser(int id);
        string Test();
    }
}