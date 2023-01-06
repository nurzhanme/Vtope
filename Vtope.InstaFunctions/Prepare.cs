using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vtope.Models;
using Vtope.Service;

namespace Vtope.InstaFunctions
{
    public class Prepare
    {
        private readonly VtopeDbContext _context;
        private readonly IJobService _jobService;

        public Prepare(VtopeDbContext context, IJobService jobService)
        {
            _context = context;
            _jobService = jobService;
        }

        [FunctionName("Prepare")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var account = await _context.InstaAccounts.FirstOrDefaultAsync(x => x.Username.Equals(name));
            await _jobService.PrepareAccount(account.Username, account.Password);


            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
