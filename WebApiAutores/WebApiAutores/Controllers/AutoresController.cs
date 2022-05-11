using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]//Decorando para hacer validaciones automaticas respecto a la data recibida en nuestro controlador
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //[HttpGet]
        //public async ActionResult<List<Autor>> Get()
        //{
        //    return new List<Autor>()
        //    {
        //        new Autor(){ Id = 1, Nombre = "Daniel"},
        //        new Autor(){ Id = 2, Nombre = "Armando"}
        //    };
        //}

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
