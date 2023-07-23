using Microsoft.EntityFrameworkCore;
using VetClinicWeb.Models;

namespace VetClinicWeb.Data
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext(DbContextOptions<VetClinicContext> options)
            : base(options)



        {
        }
        public DbSet<Clinic> Clinic { get; set; } = default!;
        public DbSet<Animal> Animal { get; set; } = default!;
        public DbSet<Medico> Medico { get; set; } = default!;
        public DbSet<Atendimento> Atendimento { get; set; } = default!;
    }    
}
