using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore6.Entities.Dtos;
using WebApiCore6.Entities;
using WebApiCore6.Interfaces;
using WebApiCore6.Repositories;

namespace WebApiCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountriesController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetAllCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetAll());
            return Ok(countries);
        }

        [HttpGet("{companies/companyId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult CountryByCompanyId(int companyId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByCompanyId(companyId));
            return Ok(country); 
        }

        [HttpGet("{companies/companyId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult CompanyByCountry(int countryId)
        {
            var companies = _mapper.Map<List<CountryDto>>(_countryRepository.GetAllCompanyByCountryId(countryId));
            return Ok(companies);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.IsCountryExist(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<CountryDto>(_countryRepository.GetById(id));
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(284)]
        [ProducesResponseType(480)]
        public IActionResult Create([FromBody] CountryDto model)
        {
            if (model == null)
                return BadRequest();

            var country = _countryRepository.GetAll().Where(i => i.Name.Trim().ToUpper() == model.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (country != null)
            {
                ModelState.AddModelError("", "country already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(model);
            if (!_countryRepository.Create(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(508, ModelState);
            }
            return Ok("Successfully");
        }


        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Edit(int countryId, [FromBody] CountryDto model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (countryId != model.Id)
                return BadRequest(ModelState);

            if (!_countryRepository.IsCountryExist(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = _mapper.Map<Country>(model);
            if (!_countryRepository.Update(countryMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int countryId)
        {
            if (!_countryRepository.IsCountryExist(countryId))
            {
                return NotFound();
            }

            var deletCountry = _countryRepository.GetById(countryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_countryRepository.Delete(deletCountry))
            {
                ModelState.AddModelError("", "Something went wroen while deleting country");
            }
            return NoContent();
        }
    }
}
