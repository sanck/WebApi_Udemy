using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores
{
    public class ApplicationDbContext : DbContext //Ctrl + . (dos veces y seleccionar constructor con options)
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Es para poder hacer querys especificos
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        //Tools - Package Manager Console
        //Commands: 
            //Add-Migration Libros
            //Update-Database
    }
}
