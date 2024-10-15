using Microsoft.EntityFrameworkCore;
using pncm.Models;

namespace pncm.Data
{
    public class ApplicationBdContext : DbContext
    {
        public ApplicationBdContext(DbContextOptions<ApplicationBdContext>options): base(options) 
        {
            
        }

        public DbSet<MarcaModel> Marca { get; set; }
        public DbSet<ModeloModel> Modelo { get; set; }
    }
}
