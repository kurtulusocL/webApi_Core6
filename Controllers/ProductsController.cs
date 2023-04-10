using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore6.Entities;
using WebApiCore6.Entities.Dtos;
using WebApiCore6.Interfaces;
using WebApiCore6.Repositories;

namespace WebApiCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetAllProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAll());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int id)
        {
            if (!_productRepository.IsProductExist(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductDto>(_productRepository.GetById(id));
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(284)]
        [ProducesResponseType(480)]
        public IActionResult Create([FromQuery] int companyId, [FromQuery] int categoryId, [FromBody] ProductDto model)
        {
            if (model == null)
                return BadRequest();

            var product = _productRepository.GetAll().Where(i => i.Name.Trim().ToUpper() == model.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (product != null)
            {
                ModelState.AddModelError("", "product already exist");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(model);

            if (!_productRepository.Create(companyId, categoryId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(508, ModelState);
            }
            return Ok("Successfully");
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Edit([FromQuery] int productId, [FromQuery] int companyId, [FromQuery] int categoryId, [FromBody] ProductDto model)
        {
            if (model == null)
                return BadRequest(ModelState);

            if (productId != model.Id)
                return BadRequest(ModelState);

            if (!_productRepository.IsProductExist(productId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productMap = _mapper.Map<Product>(model);
            if (!_productRepository.Update(companyId, categoryId, productMap))
            {
                ModelState.AddModelError("", "Someting went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int productId)
        {
            if (!_productRepository.IsProductExist(productId))
            {
                return NotFound();
            }

            var deleteProduct = _productRepository.GetById(productId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.Delete(deleteProduct))
            {
                ModelState.AddModelError("", "Something went wroen while deleting product");
            }
            return NoContent();
        }
    }
}
