using FirstMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Services.DALEmployes
{
    /// <summary>
    /// Permet d'accéder aux données relatives aux employés
    /// </summary>
    public class EmployeDbContext : DbContext
    {
        // Ce constructeur demande les options spécifiques à ce context
        // et les passe aux contructeur de la base (DbContext)
        public EmployeDbContext(DbContextOptions<EmployeDbContext> options):base(options)
        {
            
        }
        public DbSet<EmployeDAO> Employes { get; set; }
    }
}
