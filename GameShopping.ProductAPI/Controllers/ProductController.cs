using GameShopping.ProductAPI.Data.ValueObjects;
using GameShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository??throw new ArgumentNullException(nameof(_repository)); 
                
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductVO>>> GetById(long id) {

            var product = await _repository.FindById(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ProductVO>> GetAll()
        {

            var products = await _repository.FindAll();
            if (products == null) { return NotFound(); }
            return Ok(products);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody]ProductVO vo)
        {

            if (vo == null) { return BadRequest(); }
            var product = await _repository.Create(vo);
            return Ok(product);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody]ProductVO vo)
        {

           
            if (vo == null) { return BadRequest(); }
            var product = await _repository.Update(vo);
            return Ok(product);
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult>Delete(long id)
        {
            var status   = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);  
        }



    }
}
