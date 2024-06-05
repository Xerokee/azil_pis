using Azil.Common;
using Azil.Model;
using Azil.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azil.DAL.DataModel;
using Azil.Service;
using Azil.WebAPI.RESTModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Azil.WebAPI.Controllers
{
    [Route("azil")]
    public class HomeController : Controller
    {
        protected IService _service { get; private set; }
        private int _requestUserId;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IService service, ILogger<HomeController> logger)
        {
            _service = service;
            _requestUserId = -1;
            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return _service.Test();
        }

        [HttpGet]
        [Route("korisnici")]
        public IEnumerable<Korisnici> GetAllUsersDb()
        {
            IEnumerable<Korisnici> userDb = _service.GetAllUsersDb();
            return userDb;
        }

        [HttpGet]
        [Route("dnevnik_udomljavanja")]
        public IEnumerable<DnevnikUdomljavanja> GetAllUsersDb2()
        {
            IEnumerable<DnevnikUdomljavanja> userDb2 = _service.GetAllUsersDb2();
            return userDb2;
        }

        [HttpGet]
        [Route("korisnik_uloga")]
        public IEnumerable<KorisnikUloga> GetAllUsersDb3()
        {
            IEnumerable<KorisnikUloga> userDb3 = _service.GetAllUsersDb3();
            return userDb3;
        }

        [HttpGet]
        [Route("kucni_ljubimci")]
        public IEnumerable<KucniLjubimci> GetAllUsersDb4()
        {
            IEnumerable<KucniLjubimci> userDb4 = _service.GetAllUsersDb4();
            return userDb4;
        }

        [HttpGet]
        [Route("kucni_ljubimci_udomitelj")]
        public IEnumerable<KucniLjubimciUdomitelj> GetAllUsersDb5()
        {
            IEnumerable<KucniLjubimciUdomitelj> userDb5 = _service.GetAllUsersDb5();
            return userDb5;
        }

        [HttpGet]
        [Route("uloge")]
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
        [Route("Korisnici/user_id/{id_korisnika}")]
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
        }

        [HttpPost]
        [Route("Korisnici/add")]
        public async Task<IActionResult> AddUserAsync([FromBody] UsersDomain userRest)
        {
            bool lastRequestId = await GetLastUserRequestId();

            if (!lastRequestId)
            {
                _logger.LogWarning("RequestUserId not provided.");
                return BadRequest("Nije unesen RequestUserId korisnika koji poziva.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Received user: {User}", userRest);
                userRest.IdKorisnika = 0; // Ensure the ID is set to 0 or null for a new record
                bool addUser = await _service.AddUserAsync(userRest);
                if (addUser)
                {
                    _logger.LogInformation("User successfully added: {User}", userRest);
                    return Ok("Korisnik dodan!");
                }
                else
                {
                    _logger.LogWarning("Failed to add user: {User}", userRest);
                    return Ok("Korisnik nije dodan!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while adding user.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }


        [HttpPut]
        [Route("Korisnici/update/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UsersDomain userRest)
        {
            bool lastRequestId = await GetLastUserRequestId();

            if (!lastRequestId)
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
                bool updateUser = await _service.UpdateUserAsync(userRest);
                return updateUser ? Ok("Korisnik ažuriran!") : Ok("Korisnik nije ažuriran!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("Korisnici/delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            bool lastRequestId = await GetLastUserRequestId();

            if (!lastRequestId)
            {
                return BadRequest("Nije unesen RequestUserId korisnika koji poziva.");
            }

            try
            {
                bool deleteUser = await _service.DeleteUserAsync(id);
                return deleteUser ? Ok("Korisnik obrisan!") : Ok("Korisnik nije obrisan!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        #region AdditionalCustomFunctions
        [HttpGet]
        public async Task<bool> GetLastUserRequestId()
        {
            IHeaderDictionary headers = this.Request.Headers;
            if (headers.ContainsKey("RequestUserId"))
            {
                if (int.TryParse(headers["RequestUserId"].ToString(), out _requestUserId))
                {
                    return await _service.IsValidUser(_requestUserId);
                }
                return false;
            }
            return false;
        }
        #endregion AdditionalCustomFunctions
    }
}
