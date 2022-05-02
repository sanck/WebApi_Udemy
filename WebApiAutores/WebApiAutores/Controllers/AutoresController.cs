using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]//Decorando para hacer validaciones automaticas respecto a la data recibida en nuestro controlador
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>()
            {
                new Autor(){ Id = 1, Nombre = "Daniel"},
                new Autor(){ Id = 2, Nombre = "Armando"}
            };
        }
    }
}
