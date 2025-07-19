using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest("Pessoa não pode ser nula.");
            }
            // Aqui você pode adicionar lógica para salvar a pessoa no banco de dados ou processá-la conforme necessário.
            return Ok(pessoa);
        }
    }
}
