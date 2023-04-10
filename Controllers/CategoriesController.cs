using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore6.Entities;
using WebApiCore6.Entities.Dtos;
using WebApiCore6.Interfaces;

namespace WebApiCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAll());
            return Ok(categories);
        }

        [HttpGet("{product/categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult CategoryProducts(int categoryId)
        {
            var products = _mapper.Map<List<ProductDto>>(_categoryRepository.GetAllProductByCategoryId(categoryId));
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.IsCategoryExist(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<CategoryDto>(_categoryRepository.GetById(id));
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(284)]
        [ProducesResponseType(480)]
        public IActionResult Create([FromBody] CategoryDto model)
        {
            if (model == null)
                return BadRequest();

            var category = _categoryRepository.GetAll().Where(i => i.Name.Trim().ToUpper() == model.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (category != null)
            {
                ModelState.AddModelError("", "category already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(model);
            if (!_categoryRepository.Create(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(508, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Edit(int categoryId, [FromBody] CategoryDto model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (categoryId != model.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.IsCategoryExist(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Category>(model);
            if (!_categoryRepository.Update(categoryMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int categoryId)
        {
            if (!_categoryRepository.IsCategoryExist(categoryId))
            {
                return NotFound();
            }
            
            var deleteCategory = _categoryRepository.GetById(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.Delete(deleteCategory))
            {
                ModelState.AddModelError("", "Something went wroen while deleting category");
            }
            return NoContent();
        }
    }
}