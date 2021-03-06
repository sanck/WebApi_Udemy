using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{
    [ApiController]//Decorando para hacer validaciones automaticas respecto a la data recibida en nuestro controlador - permite retornar un error 400 bad request a nivel de modelo
    [Route("api/autores")] // api/Autores = api/[controller]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;

        //Acoplamiento alto descomentar las sig linea
        //public AutoresController()
        public AutoresController(ApplicationDbContext context, IServicio servicio)
        {
            //Acoplamiento alto comentar la sig linea
            this.context = context;
            this.servicio = servicio;
        }
        //
        //OBTIENE LISTA
        [HttpGet] // api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        public async Task<ActionResult<List<Autor>>> Get()
        {
            //Acoplamiento alto descomentar linea
            //var context = new ApplicationDbContext(null);
            servicio.RealizarTarea();
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }

        [HttpGet("primero")] // api/autores/primero?nombre=felipe&apellido=gavilan
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string nombre) // miValor Viene desde la cabecera, 
        {
            return await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}/{param2=persona}")] // param2? -> Opcional
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get([FromRoute] string nombre) // Con FromRoute podemos decir que el parametro viene desde la ruta (URL)
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        //INSERT
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor) //El autor va a venir desde el cuerpo de la peticion http
        {
            var existeAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if(existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //ACTUALIZAR
        [HttpPut("{id:int}")] // api/autores/[1-n]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //DELETE
        [HttpDelete("{id:int}")] // api/autores/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
