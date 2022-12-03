using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BFF.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebBFFController : ControllerBase
    {
        [HttpGet("GetAllProducts")]
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            return await GetAllProductsInternal();
        }

        private async Task<List<ProductDTO>>
        GetAllProductsInternal()
        {
            string baseURL = "http://localhost:5167/api/";
            string url = baseURL + "Product/GetAllProducts";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<ProductDTO>>(data);
                    }
                }
            }
        }
    }
}

