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
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository companyRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetAllCompanies()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetAll());
            return Ok(companies);
        }

        [HttpGet("{companyId/produuct}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult ProductByCompany(int companyId)
        {
            if (!_companyRepository.IsCompanyExist(companyId))
            {
                return BadRequest();                
            }
            var company = _mapper.Map<CompanyDto>(_companyRepository.GEtProductByCompanyId(companyId));
            return Ok(company);
        }

        [HttpGet("{company/productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        [ProducesResponseType(400)]
        public IActionResult CompanyByProduct(int productId)
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetAllCompanyByProductId(productId));
            return Ok(companies);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int id)
        {
            if (!_companyRepository.IsCompanyExist(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<CompanyDto>(_companyRepository.GetById(id));
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(284)]
        [ProducesResponseType(480)]
        public IActionResult Create([FromQuery] int id, [FromBody] CompanyDto model)
        {
            if (model == null)
                return BadRequest();

            var company = _companyRepository.GetAll().Where(i => i.Name.Trim().ToUpper() == model.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (company != null)
            {
                ModelState.AddModelError("", "company already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<Company>(model);
            companyMap.Country = _countryRepository.GetById(id);

            if (!_companyRepository.Create(companyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(508, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Edit(int companyId, [FromBody] CompanyDto model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (companyId != model.Id)
                return BadRequest(ModelState);

            if (!_companyRepository.IsCompanyExist(companyId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<Company>(model);
            if (!_companyRepository.Update(companyMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int companyId)
        {
            if (!_companyRepository.IsCompanyExist(companyId))
            {
                return NotFound();
            }

            var deleteCompany = _companyRepository.GetById(companyId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_companyRepository.Delete(deleteCompany))
            {
                ModelState.AddModelError("", "Something went wroen while deleting company");
            }
            return NoContent();
        }
    }
}
