using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            // Entity Framework com Migrations - não precisa do EnsureCreated
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}