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
        [Route("Users/user_id/{userid}")]
        public async Task<IActionResult> GetUserDomainByUserId(int userId)
        {
            HttpRequestResponse<IEnumerable<UsersDomain>> response = new HttpRequestResponse<IEnumerable<UsersDomain>>();

            Tuple<UsersDomain, List<ErrorMessage>> result = await _service.GetUserDomainByUserId(userId);

            if (result != null)
            {
                response.Result = (IEnumerable<UsersDomain>)result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
            else
            {
                response.Result = (IEnumerable<UsersDomain>)result.Item1;
                response.ErrorMessages = result.Item2;
                return Ok(response);
            }
            //UsersDomain userDomain = _service.GetUserDomainByUserId(userId);
            //return userDomain;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUserAsync([FromBody] UsersDomain userRest)
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
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        UsersDomain userDomain = new UsersDomain();
                        userDomain.Ime = userRest.Ime;
                        userDomain.Email = userRest.Email;

                        bool add_user = await _service.AddUserAsync(userDomain);

                        if (add_user)
                        {

                            return Ok("User dodan!");
                        }
                        else
                        {
                            return Ok("User nije dodan!");
                        }
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