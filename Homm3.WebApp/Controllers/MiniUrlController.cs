using Azure.Data.Tables;
using Homm3.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Homm3.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniUrlController : ControllerBase
    {
        private readonly TableClient tableClient;
        public MiniUrlController(IConfiguration configuration)
        {

            this.tableClient = new TableClient(
                configuration["TableClientConnectionString"],
                "MiniUrl");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MiniUrlContract contract)
        {
            var hash = Sha256Hash(contract.Value).Substring(0, 6); ;
            var id = Guid.NewGuid().ToString().Substring(0, 6);
            var entity = new TableEntity(hash, id)
            {
                { "value", contract.Value }
            };

            await tableClient.AddEntityAsync(entity);
            return Ok(new MiniUrlContract
            {
                Value = $"{hash}-{id}"
            });
        }

        [HttpGet("{url}")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult> Get(string url)
        {
            var parts = url.Split("-");
            var hash = parts.FirstOrDefault();
            var id = parts.LastOrDefault();
            var tableEntity = await tableClient.GetEntityAsync<TableEntity>(hash, id);
            return Ok(new MiniUrlContract
            {
                Value = tableEntity?.Value["value"]?.ToString() ?? ""
            });
        }

        public static String Sha256Hash(String value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
