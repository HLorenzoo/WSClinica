using Microsoft.EntityFrameworkCore;
using WSClinica.Models;

namespace WSClinica.Data
{
    public class DbWSClinicaContext : DbContext
    {
        public DbWSClinicaContext(DbContextOptions<DbWSClinicaContext> options) : base(options) { }

        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
    }
}
