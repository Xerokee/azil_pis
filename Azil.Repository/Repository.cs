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
        public async Task<bool> AddUserAsync(UsersDomain userDomain)
        {
            try
            {
                Korisnici userEntity = await appDbContext.Korisnici.FindAsync(userDomain.IdKorisnika);
                if (userEntity == null)
                {
                    Console.WriteLine("Mapiranje je rezultiralo u nultom entitetu.");
                    return false;
                }

                Korisnici existingUser = await appDbContext.Korisnici.FindAsync(userDomain.IdKorisnika);
                if (existingUser != null)
                {
                    Console.WriteLine("Korisnik sa istim ID-om već postoji.");
                    return false;
                }

                await appDbContext.Korisnici.AddAsync(userEntity);
                await appDbContext.SaveChangesAsync();
                Console.WriteLine("Korisnik uspješno dodan u bazi podataka.");
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Iznimka u repozitoriju AddUserAsync: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(UsersDomain userDomain)
        {
            try
            {
                Korisnici userEntity = await appDbContext.Korisnici.FindAsync(userDomain.IdKorisnika);
                if (userEntity == null) return false;

                userEntity.ime = userDomain.Ime;
                userEntity.email = userDomain.Email;
                userEntity.lozinka = userDomain.Lozinka;
                userEntity.admin = userDomain.Admin;

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