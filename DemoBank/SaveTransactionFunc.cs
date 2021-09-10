using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Service;

namespace DemoBank
{
    public class SaveTransactionFunc
    {
        private readonly ITransactionService transactionService;

        public SaveTransactionFunc(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        
        [FunctionName("transactions")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post" ,Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (req.Method.ToLower() == "post")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var result = await transactionService.SaveTransaction(requestBody);
                return new StatusCodeResult(result.ReturnCode);
            }
            if (req.Method.ToLower() == "put")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var result = await transactionService.UpdateTransaction(requestBody);
                return new StatusCodeResult(result.ReturnCode);
            }
            if (req.Method.ToLower() == "get")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var result = await transactionService.getAllTransactions();
                return new OkObjectResult(result.Body);
            }

            return new BadRequestResult();
        }
    }
}

