using AutoMapper;
using Azil.Common;
using Azil.DAL.DataModel;
using Azil.Model;
using Azil.Repository.Automapper;
using Azil.Repository.Common;
using Azil.Service.Common;
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
        IRepository _repository;
        IRepositoryMappingService _mapper;
        private readonly ILogger<Service> _logger;

        public Service(IRepository repository, IRepositoryMappingService mapper, ILogger<Service> logger)
        {
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

        public async Task<bool> AddUserAsync(UsersDomain userDomain)
        {
            try
            {
                var userEntity = _mapper.Map<Korisnici>(userDomain);
                return await _repository.AddUserAsync(userEntity);
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
        #endregion AdditionalCustomFunctions
    }
}