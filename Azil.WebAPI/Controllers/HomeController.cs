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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static Azil.WebAPI.Controllers.HomeController;
using System.IO;

namespace Azil.WebAPI.Controllers
{
    [Route("azil")]
    public class HomeController : Controller
    {
        protected IService _service { get; private set; }
        private int _requestUserId;
        private int _requestAnimalId;
        private readonly ILogger<HomeController> _logger;
        private readonly Azil_DbContext _context;

        public HomeController(IService service, ILogger<HomeController> logger, Azil_DbContext context)
        {
            _service = service;
            _requestUserId = -1;
            _requestAnimalId = -1;
            _logger = logger;
            _context = context;
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
        public List<KucniLjubimciDomain> GetAllUsersDb4()
        {
            List<KucniLjubimciDomain> userDb4 = _service.GetKucniLjubimci();
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
        [Route("aktivnosti")]
        public IEnumerable<Aktivnosti> GetAllUsersDb7()
        {
            IEnumerable<Aktivnosti> userDb7 = _service.GetAllUsersDb7();
            return userDb7;
        }

        [HttpGet]
        [Route("slike")]
        public IEnumerable<Slika> GetAllUsersDb8()
        {
            IEnumerable<Slika> userDb8 = _service.GetAllUsersDb8();
            return userDb8;
        }

        [HttpGet]
        [Route("sifrTipLjubimca")]
        public IEnumerable<SifrTipLjubimca> GetAllUsersDb9()
        {
            IEnumerable<SifrTipLjubimca> userDb9 = _service.GetAllUsersDb9();
            return userDb9;
        }

        [HttpGet]
        [Route("sifrBojaLjubimca")]
        public IEnumerable<SifrBojaLjubimca> GetAllUsersDb10()
        {
            IEnumerable<SifrBojaLjubimca> userDb10 = _service.GetAllUsersDb10();
            return userDb10;
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
            var animals = await _service.GetAllAnimalsWithImages();

            // Filtriraj životinje koje nisu udomljene
            var availableAnimals = animals.Where(a => !a.udomljen);

            return Ok(availableAnimals);
        }

        [HttpGet]
        [Route("galerija_zivotinja")]
        public async Task<IActionResult> GetAllAnimalGallery()
        {
            var gallery = await _service.GetAllAnimalGallery();
            return Ok(gallery);
        }

        [HttpGet]
        [Route("KucniLjubimci/{id}/galerija")]
        public async Task<IActionResult> GetGalleryByAnimalId(int id)
        {
            _logger.LogInformation($"Received request for animal gallery with ID: {id}");

            var galerija = await _service.GetGalleryByAnimalId(id);

            if (galerija == null || !galerija.Any())
            {
                _logger.LogWarning($"No gallery found for animal with ID: {id}");
                return NotFound($"No gallery found for animal with ID {id}.");
            }

            return Ok(galerija);
        }

        // Dohvaćanje životinja prema tipu
        [HttpGet("animals")]
        public ActionResult<List<KucniLjubimci>> GetAnimals([FromQuery] int? type)
        {
            List<KucniLjubimci> animals;

            if (type == null)
            {
                // Ako nema query parametra, vraćamo i pse i mačke
                animals = _service.GetDogsAndCats();
            }
            else
            {
                // Ako postoji parametar, dohvaćamo samo taj tip životinje
                animals = _service.GetAnimalsByType(type.Value);
            }

            if (animals == null || animals.Count == 0)
            {
                return NotFound("Nema pronađenih životinja.");
            }
            return Ok(animals);
        }

        [HttpGet]
        [Route("KucniLjubimci/{type}")]
        public async Task<IActionResult> GetAnimalsByType(string type)
        {
            if (type.ToLower() != "pas" && type.ToLower() != "mačka")
            {
                return BadRequest("Tip životinje mora biti 'pas' ili 'mačka'.");
            }

            // Dohvati odgovarajući id tipa ljubimca iz šifrarnika
            var tipLjubimca = await _context.Sifrarnik
                                                 .Where(s => s.naziv.ToLower() == type.ToLower())
                                                 .Select(s => s.id)
                                                 .FirstOrDefaultAsync();

            if (tipLjubimca == 0)
            {
                return NotFound($"Tip ljubimca '{type}' nije pronađen.");
            }

            // Dohvati životinje po tipu i statusu udomljavanja
            var animals = await _service.GetAnimalsByTypeAndAdoptionStatus(tipLjubimca);

            if (animals == null || !animals.Any())
            {
                return NotFound($"No animals of type {type} found.");
            }

            return Ok(animals);
        }

        [HttpGet]
        [Route("KucniLjubimci/{color}")]
        public async Task<IActionResult> GetAnimalsByColor(string color)
        {
            if (color.ToLower() != "crna" && color.ToLower() != "bijela" && color.ToLower() != "smeđa")
            {
                return BadRequest("Tip životinje mora biti 'pas' ili 'mačka'.");
            }

            // Dohvati odgovarajući id tipa ljubimca iz šifrarnika
            var bojaLjubimca = await _context.Sifrarnik2
                                                 .Where(s => s.naziv.ToLower() == color.ToLower())
                                                 .Select(s => s.id)
                                                 .FirstOrDefaultAsync();

            if (bojaLjubimca == 0)
            {
                return NotFound($"Boja ljubimca '{color}' nije pronađen.");
            }

            // Dohvati životinje po tipu i statusu udomljavanja
            var animals = await _service.GetAnimalsByColorAndAdoptionStatus(bojaLjubimca);

            if (animals == null || !animals.Any())
            {
                return NotFound($"No animals of color {color} found.");
            }

            return Ok(animals);
        }

        [HttpGet("KucniLjubimci/{id:int}")]
        public async Task<IActionResult> GetAnimalById([FromRoute] int id)
        {
            _logger.LogInformation("Fetching animal with ID: {Id}", id);
            var animal = await _service.GetAnimalById(id);

            if (animal == null)
            {
                _logger.LogWarning("Animal with ID {Id} not found.", id);
                return NotFound($"Animal with ID {id} not found.");
            }

            return Ok(animal);
        }

        [HttpPut]
        [Route("KucniLjubimci/{id}/udomi")]
        public async Task<IActionResult> AdoptAnimal(int id)
        {


            // Ažuriraj status na udomljen
            //animalDb.zahtjev_udomljen = true;

            // Spremi promenu u bazu
            //bool result = await _service.UpdateAnimalAsync(animalDb);
            bool result = await _service.AdoptAnimalAsync(id);
            if (result)
            {
                return Ok("Ljubimac je uspješno udomljen.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom udomljavanja.");
            }
        }


        [HttpGet("GetFilteredAnimalsByAgeRange")]
        public async Task<IActionResult> GetFilteredAnimalsByAgeRange(string tipLjubimca, int? minDob, int? maxDob, int? dob, string boja)
        {
            try
            {
                _logger.LogInformation("Fetching filtered animals with parameters: Tip: {TipLjubimca}, MinDob: {MinDob}, MaxDob: {MaxDob}, Dob: {Dob}, Boja: {Boja}",
                    tipLjubimca, minDob, maxDob, dob, boja);

                // Ako je tipLjubimca string (npr. "Pas"), treba ga pretvoriti u ID
                int? tipLjubimcaId = await _service.GetTipLjubimcaId(tipLjubimca);
                if (tipLjubimcaId == null)
                {
                    return BadRequest("Tip ljubimca mora biti 'Pas' ili 'Mačka'.");
                }

                // Ako je bojaLjubimca string (npr. "Crna"), treba ga pretvoriti u ID
                int? bojaLjubimcaId = await _service.GetBojaLjubimcaId(boja);
                if (bojaLjubimcaId == null)
                {
                    return BadRequest("Boja ljubimca mora biti 'Crna', Bijela ili 'Smeđa'.");
                }

                var animals = await _service.GetFilteredAnimalsByAgeRange(tipLjubimcaId.Value, minDob, maxDob, dob, bojaLjubimcaId.Value);

                if (animals == null || !animals.Any())
                {
                    _logger.LogInformation("No animals found with the given filter.");
                    return NotFound("No animals found with the given filter.");
                }

                return Ok(animals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filtering animals.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom dohvaćanja filtriranih životinja.");
            }
        }


        [HttpPut]
        [Route("KucniLjubimci/{id}/odbij")]
        public async Task<IActionResult> RejectAnimal(int id)
        {
            // Spremi promenu u bazu
            bool result = await _service.RejectAnimalAsync(id);
            if (result)
            {
                return Ok("Ljubimac je uspješno odbijen.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom odbijanja.");
            }
        }

        [HttpPost]
        [Route("KucniLjubimci/add")]
        public async Task<IActionResult> AddAnimalAsync([FromBody] AnimalsDomain animalRest)
        {
            _logger.LogInformation("Primljeni podaci za dodavanje životinje: {@AnimalRest}", animalRest);

            // Validacija RequestAnimalId
            bool lastRequestId = await GetLastAnimalRequestId();
            if (!lastRequestId)
            {
                _logger.LogWarning("RequestAnimalId nije pronađen.");
                return BadRequest("RequestAnimalId nije unesen ili je nevažeći.");
            }

            // Validacija ModelState
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state nije ispravan: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            // Validacija tipa ljubimca
            if (animalRest.TipLjubimca != 1 && animalRest.TipLjubimca != 2)
            {
                _logger.LogWarning("Neispravan tip ljubimca: {TipLjubimca}", animalRest.TipLjubimca);
                return BadRequest("Tip ljubimca mora biti 1 (Pas) ili 2 (Mačka).");
            }

            // Validacija boje ljubimca
            if (animalRest.Boja != 1 && animalRest.Boja != 2 && animalRest.Boja != 3)
            {
                _logger.LogWarning("Neispravna boja ljubimca: {Boja}", animalRest.Boja);
                return BadRequest("Boja ljubimca mora biti 1 (Crna), 2 (Bijela) ili 3 (Smeđa).");
            }

            try
            {
                _logger.LogInformation("Zapoceto dodavanje životinje: {@AnimalRest}", animalRest);

                animalRest.IdLjubimca = 0; // Resetiranje ID-a za novi zapis
                bool addAnimal = await _service.AddAnimalAsync(animalRest);

                if (addAnimal)
                {
                    _logger.LogInformation("Životinja uspješno dodana: {@AnimalRest}", animalRest);
                    return Ok("Životinja uspješno dodana!");
                }
                else
                {
                    _logger.LogWarning("Dodavanje životinje nije uspjelo: {@AnimalRest}", animalRest);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dodavanje životinje nije uspjelo.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška pri dodavanju životinje.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška na serveru.");
            }
        }

        [HttpPost]
        [Route("DnevnikUdomljavanja/add")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
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
            /*
            bool lastRequestId = await GetLastAnimalRequestId();

            if (!lastRequestId)
            {
                return BadRequest("Nije unesen RequestAnimalId životinje koji poziva.");
            }
            */

            try
            {
                bool result = await _service.DeleteAdoptionAsync(id);
                if (result)
                {
                    return Ok("Uspješno obrisano!");
                }
                {
                    return NotFound(new { message = "Zapis nije pronađen ili je već obrisan." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DnevnikUdomljavanja/{idLjubimca}/vrati/{idUdomljavanja}")]
        public async Task<IActionResult> UpdateAdoptionStatus(int idLjubimca, int idUdomljavanja)
        {
            try
            {
                bool result = await _service.UpdateAdoptionStatus(idLjubimca, idUdomljavanja);
                if (result)
                {
                    return Ok("Uspješno vraćeno!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Greška u vraćanju!");
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

        // Dohvaćanje statusa udomljavanja prema id_korisnika
        [HttpGet("DnevnikUdomljavanja/{idKorisnika}/status")]
        public async Task<IActionResult> GetAdoptionStatusByUserId(int idKorisnika)
        {
            var adoption = await _service.GetAdoptionStatusByUserId(idKorisnika);
            return Ok(adoption?.status_udomljavanja);
        }

        // Ažuriranje statusa udomljavanja prema id_korisnika
        [HttpPut]
        [Route("DnevnikUdomljavanja/{idKorisnika}/update/status")]
        public async Task<IActionResult> SetAdoptionStatusByUserId(int idKorisnika, [FromBody] AdoptionStatusUpdateRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Request model state is invalid.");
                return BadRequest("Neispravni podaci.");
            }

            bool result = await _service.SetAdoptionStatusByUserId(idKorisnika, request.status_udomljavanja);
            if (result)
            {
                return Ok("Status udomljavanja uspješno ažuriran.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška u ažuriranju statusa udomljavanja.");
            }
        }

        [HttpGet]
        [Route("odbijene_zivotinje")]
        public async Task<IActionResult> GetAllRejections()
        {
            var rejections = await _service.GetAllRejections();
            return Ok(rejections);
        }

        public class AdoptionStatusUpdateRequest
        {
            public bool status_udomljavanja { get; set; }
        }

        [HttpPost]
        [Route("OdbijeneZivotinje")]
        public async Task<IActionResult> SaveRejection([FromBody] RejectionRequest request)
        {
            if (request == null || request.IdKorisnika <= 0 || request.IdLjubimca <= 0 || string.IsNullOrEmpty(request.ImeLjubimca))
            {
                return BadRequest("Neispravni podaci.");
            }

            bool result = await _service.SaveRejectionAsync(request.IdKorisnika, request.IdLjubimca, request.ImeLjubimca);
            if (result)
            {
                return Ok("Odbijanje uspješno spremljeno.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri spremanju odbijanja.");
            }
        }

        // Metoda za brisanje unosa iz tabele odbijene_zivotinje prema ID-u
        [HttpDelete]
        [Route("OdbijeneZivotinje/{id}")]
        public async Task<IActionResult> DeleteRejection(int id)
        {
            bool result = await _service.DeleteRejectionAsync(id);
            if (result)
            {
                return Ok("Odbijanje uspješno obrisano.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška pri brisanju odbijanja.");
            }
        }

        [HttpPost]
        [Route("KucniLjubimci/{id}/update")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] UpdateKucniLjubimac animalUpdate)
        {
            KucniLjubimci animal = new KucniLjubimci();
            animal.ime_ljubimca = animalUpdate.ime_ljubimca;
            animal.dob = animalUpdate.dob;
            animal.opis_ljubimca = animalUpdate.opis_ljubimca;

            // Spremi promenu u bazu
            bool result = await _service.UpdateAnimal(animal, id);
            if (result)
            {
                return Ok("Ljubimac je uspješno udomljen.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom udomljavanja.");
            }
        }

        [HttpGet]
        [Route("Aktivnosti/{id_ljubimca}")]
        public async Task<IActionResult> GetAktivnostiById(int id_ljubimca)
        {
            HttpRequestResponse<List<ActivityDomain>> response = new HttpRequestResponse<List<ActivityDomain>>();

            Tuple<List<ActivityDomain>, List<ErrorMessage>> result = await _service.GetAktivnostiById(id_ljubimca);

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
        [Route("SifrarnikTipLjubimca")]
        public async Task<IActionResult> GetSifrarnikTipLjubimca()
        {
            var sifrarnik = await _service.GetSifrarnik();
            return Ok(sifrarnik);
        }

        [HttpGet]
        [Route("SifrarnikBojaLjubimca")]
        public async Task<IActionResult> GetSifrarnikBojaLjubimca()
        {
            var sifrarnik2 = await _service.GetSifrarnik2();
            return Ok(sifrarnik2);
        }

        public class UpdateKucniLjubimac
        {
            public string ime_ljubimca { get; set; }
            public int dob { get; set; }
            public string opis_ljubimca { get; set; }
        }

        public class AktivnostiModel
        {
            public int id { get; set; }
            public int id_ljubimca { get; set; }
            public string datum { get; set; }
            public string aktivnost { get; set; }
            public string opis { get; set; }
        }

        public class RejectionRequest
        {
            public int IdKorisnika { get; set; }
            public string ImeLjubimca { get; set; }
            public int IdLjubimca { get; set; }
        }

        [HttpPost]
        [Route("Aktivnosti/add")]
        public async Task<IActionResult> AddAktivnostAsync([FromBody] AktivnostiModel aktivnostRest)
        {
            try
            {
                Aktivnosti novaAktivnost = new Aktivnosti();
                novaAktivnost.id_ljubimca = aktivnostRest.id_ljubimca;
                string format = "dd-MM-yyyy";
                DateTime dateTimeDatum = DateTime.ParseExact(aktivnostRest.datum, format, CultureInfo.InvariantCulture);
                novaAktivnost.datum = dateTimeDatum;
                novaAktivnost.aktivnost = aktivnostRest.aktivnost;
                novaAktivnost.opis = aktivnostRest.opis;

                bool addAktivnost = await _service.AddAktivnostAsync(novaAktivnost);
                if (addAktivnost)
                {
                    _logger.LogInformation("Activity successfully added.");
                    return Ok("Aktivnost dodana!");
                }
                else
                {
                    _logger.LogWarning("Failed to add activity.");
                    return Ok("Aktivnost nije dodana!");
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while adding activity.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("Slike/add/{id_ljubimca}")]
        public async Task<IActionResult> AddImage(int id_ljubimca, IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0) { return BadRequest("No file was uploaded."); }

                var novaSlika = new Slika();
                novaSlika.id_ljubimca = id_ljubimca;
                novaSlika.slika_data = await ConvertToBytes(image);

                bool result = await _service.AddImage(novaSlika);

                if (result)
                {
                    _logger.LogInformation("Image successfully added.");
                    return Ok("Slika uspješno dodana!");
                }
                else
                {
                    _logger.LogWarning("Failed to add image.");
                    return Ok("Slika nije dodana!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while adding activity.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        private async Task<byte[]> ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpGet]
        [Route("Slike/{id_ljubimca}")]
        public async Task<IActionResult> GetSlikeById(int id_ljubimca)
        {
            HttpRequestResponse<List<SlikaDomain>> response = new HttpRequestResponse<List<SlikaDomain>>();

            Tuple<List<SlikaDomain>, List<ErrorMessage>> result = await _service.GetSlikeById(id_ljubimca);

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

        [HttpDelete]
        [Route("Slike/delete/{id}")]
        public async Task<IActionResult> DeleteSlikaAsync(int id)
        {
            try
            {
                bool result = await _service.DeleteSlikaAsync(id);

                if (result)
                {
                    _logger.LogInformation("Image successfully deleted.");
                    return Ok("Slika uspješno obrisana!");
                }
                else
                {
                    _logger.LogWarning("Failed to delete image.");
                    return Ok("Slika nije obrisana!");
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while deleting image.");
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("KucniLjubimci/{id}/udomljen")]
        public async Task<IActionResult> AdoptAnimalByAdmin(int id)
        {
            bool result = await _service.AdoptAnimalByAdmin(id);
            if (result)
            {
                return Ok("Ljubimac je uspješno udomljen.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greška prilikom udomljavanja.");
            }
        }

        [HttpGet]
        [Route("Meetings")]
        public async Task<IActionResult> GetMeetings()
        {
            HttpRequestResponse<List<Meeting>> response = new HttpRequestResponse<List<Meeting>>();

            Tuple<List<Meeting>, List<ErrorMessage>> result = await _service.GetMeetings();

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

        [HttpGet]
        [Route("Statistika")]
        public async Task<IActionResult> GetStatistika()
        {
            HttpRequestResponse<StatistikaDomain> response = new HttpRequestResponse<StatistikaDomain>();

            Tuple<StatistikaDomain, List<ErrorMessage>> result = _service.GetStatistika();

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
        [Route("Token/{email}")]
        public async Task<IActionResult> GetToken(string email)
        {
            string result = await _service.GetToken(email);

            return Ok(result);
        }
        #endregion AdditionalCustomFunctions
    }
}