using Azil.Common;
using Azil.Model;
using Azil.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azil.DAL.DataModel;
using Azil.Service;
using Azil.WebAPI.RESTModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Azil.WebAPI.Controllers
{
    [Route("azil")]
    public class HomeController : Controller
    {
        protected IService _service { get; private set; }
        private int _requestUserId;

        public HomeController(IService service)
        {
            _service = service;
            _requestUserId = -1;
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return _service.Test();
        }

        [HttpGet]
        [Route("Users_db")]
        public IEnumerable<Korisnici> GetAllUsersDb()
        {
            IEnumerable<Korisnici> userDb = _service.GetAllUsersDb();

            return userDb;
        }

        [HttpGet]
        [Route("Users_db2")]
        public IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2()
        {
            IEnumerable<DnevnikUdomljavanja> userDb2 = _service.GetAllUsersDb2();

            return userDb2;
        }

        [HttpGet]
        [Route("Users_db3")]
        public IEnumerable<KorisnikUloga> GetAllUsersDb3()
        {
            IEnumerable<KorisnikUloga> userDb3 = _service.GetAllUsersDb3();

            return userDb3;
        }

        [HttpGet]
        [Route("Users_db4")]
        public IEnumerable<KucniLjubimci> GetAllUsersDb4()
        {
            IEnumerable<KucniLjubimci> userDb4 = _service.GetAllUsersDb4();

            return userDb4;
        }

        [HttpGet]
        [Route("Users_db5")]
        public IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5()
        {
            IEnumerable<KucniLjubimciUdomitelj> userDb5 = _service.GetAllUsersDb5();

            return userDb5;
        }

        [HttpGet]
        [Route("Users_db6")]
        public IEnumerable<Uloge> GetAllUsersDb6()
        {
            IEnumerable<Uloge> userDb6 = _service.GetAllUsersDb6();

            return userDb6;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            HttpRequestResponse<IEnumerable<UsersDomain>> response = new HttpRequestResponse<IEnumerable<UsersDomain>>();

            Tuple<IEnumerable<UsersDomain>, List<ErrorMessage>> result = await _service.GetAllUsers();

            if (result != null)
            {
                response.Result = result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
            else
            {
                response.Result = result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("Users/user_id/{id_korisnika}")]
        public async Task<IActionResult> GetUserDomainByUserId(int id_korisnika)
        {
            HttpRequestResponse<UsersDomain> response = new HttpRequestResponse<UsersDomain>();

            Tuple<UsersDomain, List<ErrorMessage>> result = await _service.GetUserDomainByUserId(id_korisnika);

            if (result != null)
            {
                response.Result = result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
            else
            {
                response.Result = result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
            //UsersDomain userDomain = _service.GetUserDomainByUserId(userId);
            //return userDomain;
        }

        [HttpPost]
        [Route("Users/add/{id}")]
        public async Task<IActionResult> AddUserAsync(int id, [FromBody] UsersDomain userRest)
        {
            bool lastrequestId = await GetLastUserRequestId();

            if (!lastrequestId)
            {
                return BadRequest("Nije unesen RequestUserId korisnika koji poziva.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userRest.IdKorisnika = id;  // Assign the ID from the route to the userRest object
                bool add_user = await _service.AddUserAsync(userRest);
                if (add_user)
                {
                    Console.WriteLine("Korisnik uspješno dodan.");
                    return Ok("Korisnik dodan!");
                }
                else
                {
                    Console.WriteLine("Korisnik nije dodan. Provjeri metodu repozitorija.");
                    return Ok("Korisnik nije dodan!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("Users/update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UsersDomain userRest)
        {
            bool lastrequestId = await GetLastUserRequestId();

            if (!lastrequestId)
            {
                return BadRequest("Nije unesen RequestUserId korisnika koji poziva.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userRest.IdKorisnika = id;  // Assign the ID from the route to the userRest object
                bool update_user = await _service.UpdateUserAsync(userRest);
                return update_user ? Ok("Korisnik ažuriran!") : Ok("Korisnik nije ažuriran!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }


        [HttpDelete]
        [Route("Users/delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            bool lastrequestId = await GetLastUserRequestId();

            if (!lastrequestId)
            {
                return BadRequest("Nije unesen RequestUserId korisnika koji poziva.");
            }
            else
            {
                try
                {
                    bool delete_user = await _service.DeleteUserAsync(id);

                    if (delete_user)
                    {
                        return Ok("Korisnik obrisan!");
                    }
                    else
                    {
                        return Ok("Korisnik nije obrisan!");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
                }
            }
        }

        #region AdditionalCustomFunctions
        public async Task<bool> GetLastUserRequestId()
        {
            IHeaderDictionary headers = this.Request.Headers;
            if (headers.ContainsKey("RequestUserId"))
            {
                if (int.TryParse(headers["RequestUserId"].ToString(), out _requestUserId))
                {
                    return await _service.IsValidUser(_requestUserId);
                    //return true;
                }
                else return false;
            }
            return false;

        }
        #endregion AdditionalCustomFunctions
    }
}