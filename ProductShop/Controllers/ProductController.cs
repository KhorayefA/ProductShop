using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using ProductShop.Data;
using System.Web.Http.Cors;

namespace ProductShop.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        
        private IProductRepository _repository;

        public ProductController()
        {
            _repository = new ProductRepository();
        }
        //public IHttpActionResult Put()
        //{
        //    return BadRequest("No thing bad baby");
        //}

        public void POST(Product product)
        {
            _repository.AddProduct(product);
            _repository.SaveChangesAsync();
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllProductsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        public async Task<IHttpActionResult> Delete(int productId)
        {
            try
            {
                var product = await _repository.GetProductAsync(productId);
                if (product == null) return NotFound("There is no product with this ID");

                _repository.DeleteProduct(product);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok(product + "\n This product has been dealted");
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        private IHttpActionResult NotFound(string v)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> Put(int productId, Product newProduct)
        {
            try
            {
                var oldProduct = await _repository.GetProductAsync(productId);
                if (oldProduct == null) return NotFound();

                _repository.Map(oldProduct, newProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(oldProduct);
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //public object Get()
        //{

        //    return new { Name = "AbuYahya", Age = "25" };
        //}

        //public IHttpActionResult Get()
        //{
        //    return BadRequest("No thing bad baby");
        //    //return Ok(new { Name = "AbuYahya", Age = "25" });
        //}

        ////GET: Product
        //public string Index()
        //{
        //    return "test";
        //}

        //private IProductRepository _repository;

        //public ProductController(IProductRepository repository)
        //{
        //    _repository = repository;

        //}
        //public async Task<IHttpActionResult> Get()
        //{
        //    try
        //    {
        //        var result = await _repository.GetAllCampsAsync();

        //        //Mapping
        //        var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

        //        return Ok(mappedResult);
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}
        //[Route("{id}")]
        //public async Task<IHttpActionResult> Get(int id)
        //{
        //    try
        //    {
        //        var result = await _repository.GetCampAsync(id);

        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        return InternalServerError();
        //    }
        //}
    }
}

