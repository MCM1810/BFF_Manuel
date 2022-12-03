using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BFF.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileBFFController : ControllerBase
    {
        [HttpGet("GetAllCustomers")]
        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            return await GetAllCustomersAsync();
        }

        private async Task<List<CustomerDTO>>
        GetAllCustomersAsync()
        {
            string baseURL = "http://localhost:5034/api/";
            string url = baseURL + "Customer/GetAllCustomers";

            using (HttpClient client = new HttpClient())
            {
                List<CustomerDTO> customerDTOs = new List<CustomerDTO>();

                using (HttpResponseMessage responseMessage = await client.GetAsync(url))
                {
                    using (HttpContent content = responseMessage.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        var customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(data);

                        foreach (var customer in customers)
                        {
                            CustomerDTO customerDTO = new CustomerDTO();
                            customerDTO.Id = customer.Id;
                            customerDTO.FirstName = customer.FirstName;
                            customerDTO.LastName = customer.LastName;
                            customerDTO.Phone = null;
                            customerDTO.Address = null;
                            customerDTO.EmailAddress = null;
                            customerDTOs.Add(customerDTO);
                        }

                        return customerDTOs;
                    }
                }
            }
        }
    }
}
