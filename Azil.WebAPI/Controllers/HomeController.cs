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
using AutoMapper;

namespace Azil.WebAPI.Controllers
{
    [Route("azil")]
    public class HomeController : Controller
    {
        protected IService _service { get; private set; }
        private int _requestUserId;
        private int _requestAnimalId;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IService service, ILogger<HomeController> logger)
        {
            _service = service;
            _requestUserId = -1;
            _requestAnimalId = -1;
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

        [HttpGet]
        [Route("Korisnici/role/{id_korisnika}")]
        public async Task<IActionResult> GetUserRoleById(int id_korisnika)
        {
            var userRole = await _service.GetUserRoleById(id_korisnika);
            if (userRole != null)
            {
                return Ok(userRole);
            }
            return NotFound();
        }


        [HttpGet]
        [Route("Korisnici/email/{email}")]
        public async Task<IActionResult> GetUserDomainByEmail(string email)
        {
            HttpRequestResponse<UsersDomain> response = new HttpRequestResponse<UsersDomain>();

            Tuple<UsersDomain, List<ErrorMessage>> result = await _service.GetUserDomainByEmail(email);

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
        [Route("Korisnici/lozinka/{email}")]
        public async Task<IActionResult> GetPasswordByEmail(string email)
        {
            _logger.LogInformation("Poziv metode GetPasswordByEmail s emailom: {Email}", email);

            // Dohvati tuple koji sadrži korisnika i liste grešaka
            var result = await _service.GetUserDomainByEmail(email);
            var user = result.Item1;
            var errorMessages = result.Item2;

            if (user != null)
            {
                _logger.LogInformation("Korisnik pronađen: {User}", user);
                return Ok(user.Lozinka);  // Vrati samo lozinku kao string
            }
            else
            {
                _logger.LogWarning("Korisnik nije pronađen za email: {Email}", email);
                return NotFound("Korisnik nije pronađen");
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
                userRest.id_korisnika = 0; // Ensure the ID is set to 0 or null for a new record
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
                userRest.id_korisnika = id;  // Assign the ID from the route to the userRest object
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

        [HttpGet]
        [Route("KucniLjubimci")]
        public async Task<IActionResult> GetAllAnimals()
        {
            var animals = await _service.GetAllAnimals();
            return Ok(animals);
        }

        [HttpGet]
        [Route("KucniLjubimci/{type}")]
        public async Task<IActionResult> GetAnimalsByType(string type)
        {
            var animals = await _service.GetAnimalsByType(type);
            return Ok(animals);
        }

        [HttpPost]
        [Route("KucniLjubimci/add")]
        public async Task<IActionResult> AddAnimalAsync([FromBody] AnimalsDomain animalRest)
        {
            bool lastRequestId = await GetLastAnimalRequestId();

            if (!lastRequestId)
            {
                _logger.LogWarning("RequestAnimalId not provided.");
                return BadRequest("Nije unesen RequestAnimalId životinje koji poziva.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Received animal: {Animal}", animalRest);
                animalRest.IdLjubimca = 0; // Ensure the ID is set to 0 or null for a new record
                bool addAnimal = await _service.AddAnimalAsync(animalRest);
                if (addAnimal)
                {
                    _logger.LogInformation("Animal successfully added: {Animal}", animalRest);
                    return Ok("Životinja dodana!");
                }
                else
                {
                    _logger.LogWarning("Failed to add animal: {Animal}", animalRest);
                    return Ok("Životinja nije dodana!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while adding animal.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("DnevnikUdomljavanja/add")]
        public async Task<IActionResult> AddAdoptionAsync([FromBody] DnevnikUdomljavanja adoption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _service.AddAdoptionAsync(adoption);
                if (result)
                {
                    return Ok("Uspješno dodano!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška u dodavanju!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DnevnikUdomljavanja/update/{id}")]
        public async Task<IActionResult> UpdateAdoptionAsync(int id, [FromBody] DnevnikUdomljavanja adoption)
        {
            bool lastRequestId = await GetLastAnimalRequestId();

            if (!lastRequestId)
            {
                return BadRequest("Nije unesen RequestAnimalId životinje koji poziva.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                adoption.id_ljubimca = id;
                bool result = await _service.UpdateAdoptionAsync(adoption);
                if (result)
                {
                    return Ok("Uspješno ažurirano!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška u ažuriranju!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("DnevnikUdomljavanja/delete/{id}")]
        public async Task<IActionResult> DeleteAdoptionAsync(int id)
        {
            bool lastRequestId = await GetLastAnimalRequestId();

            if (!lastRequestId)
            {
                return BadRequest("Nije unesen RequestAnimalId životinje koji poziva.");
            }

            try
            {
                bool result = await _service.DeleteAdoptionAsync(id);
                if (result)
                {
                    return Ok("Uspješno obrisano!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška u brisanju!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("AdoptedAnimals")]
        public async Task<IActionResult> GetAdoptedAnimals()
        {
            var animals = await _service.GetAdoptedAnimals();
            return Ok(animals);
        }

        [HttpGet("{idLjubimca}/status")]
        public async Task<IActionResult> GetAdoptionStatus(int idLjubimca)
        {
            bool status = await _service.GetAdoptionStatus(idLjubimca);
            return Ok(status);
        }

        [HttpPut]
        [Route("DnevnikUdomljavanja/{idLjubimca}/update/status")]
        public async Task<IActionResult> SetAdoptionStatus(int idLjubimca, [FromBody] AdoptionStatusUpdateRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Request model state is invalid.");
                return BadRequest("Neispravni podaci.");
            }

            bool result = await _service.SetAdoptionStatus(idLjubimca, request.status_udomljavanja);
            if (result)
            {
                return Ok("Status udomljavanja uspješno ažuriran.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška u ažuriranju statusa udomljavanja.");
            }
        }

        public class AdoptionStatusUpdateRequest
        {
            public bool status_udomljavanja { get; set; }
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

        [HttpGet]
        public async Task<bool> GetLastAnimalRequestId()
        {
            IHeaderDictionary headers = this.Request.Headers;
            if (headers.ContainsKey("RequestAnimalId"))
            {
                if (int.TryParse(headers["RequestAnimalId"].ToString(), out _requestAnimalId))
                {
                    return await _service.IsValidAnimal(_requestAnimalId);
                }
                return false;
            }
            return false;
        }
        #endregion AdditionalCustomFunctions
    }
}
