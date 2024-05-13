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

namespace Azil.Repository
{
    public class Repository : IRepository
    {
        private readonly Azil_DbContext appDbContext;
        private IRepositoryMappingService _mapper;

        public Repository(Azil_DbContext appDbContext, IRepositoryMappingService mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
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
        public UsersDomain GetUserDomainByUserId(int userId)
        {
            Korisnici userDb = appDbContext.Korisnici.Find(userId);

            UsersDomain user = _mapper.Map<UsersDomain>(userDb);

            return user;
        }
        public async Task<bool> AddUserAsync(UsersDomain userDomain)
        {
            try
            {
                EntityEntry<Korisnici> user_created = await appDbContext.Korisnici.AddAsync(
                        _mapper.Map<Korisnici>(userDomain));
                await appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
    }
}