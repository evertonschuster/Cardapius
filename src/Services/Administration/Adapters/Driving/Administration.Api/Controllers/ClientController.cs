using Administration.Application.Clients.Commands;
using BuildingBlocks.Observability.Traces;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        public ClientController(ILogger<ClientController> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ILogger<ClientController> Logger { get; private set; }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateClientCommand command)
        {
            //https://www.elastic.co/guide/en/apm/agent/dotnet/current/public-api.html
            var transaction = Elastic.Apm.Agent.Tracer;


            var span = Elastic.Apm.Agent.Tracer.CurrentTransaction.StartSpan("Salame 2", "Teste");

            //Thread.Sleep(2000);

            Elastic.Apm.Agent.Tracer.CurrentTransaction.SetLabel("stringSample", "bar");
            Elastic.Apm.Agent.Tracer.CurrentTransaction.SetLabel("boolSample", true);
            Elastic.Apm.Agent.Tracer.CurrentTransaction.SetLabel("intSample", 42);

            span.End();


            Logger.LogCritical("LogCritical");
            Logger.LogError("LogError");
            Logger.LogTrace("LogTrace");


            await Elastic.Apm.Agent.Tracer
            .CaptureTransaction("TestTransaction", "TestType", async () =>
            {
                //application code that is captured as a transaction
                await Task.Delay(500); //sample async code
            });


            return Ok(command);
        }

        [HttpPost("Post3Async")]
        public async Task<IActionResult> Post3Async([FromBody] CreateClientCommand command, [FromServices] ITracer tracer)
        {
            //https://www.elastic.co/guide/en/apm/agent/dotnet/current/public-api.html
            //var transaction = Elastic.Apm.Agent.Tracer;


            var span = tracer.StartSpan("Salame 2", "Teste");

            Thread.Sleep(2000);

            tracer.SetLabel("stringSample", "bar");
            tracer.SetLabel("boolSample", true);
            tracer.SetLabel("intSample", 42);

            span.End();


            Logger.LogCritical("LogCritical");
            Logger.LogError("LogError");
            Logger.LogTrace("LogTrace");


            //await Elastic.Apm.Agent.Tracer
            //.CaptureTransaction("TestTransaction", "TestType", async () =>
            //{
            //    //application code that is captured as a transaction
            //    await Task.Delay(500); //sample async code
            //});


            return Ok(command);
        }

        [HttpPost("Post1")]
        public IActionResult Post1([FromBody] CreateClientCommand command)
        {
            throw new Exception("Exception");
            return Ok(command);
        }
    }
}
