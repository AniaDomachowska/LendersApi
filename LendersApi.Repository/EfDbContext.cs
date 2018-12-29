using LendersApi.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace LendersApi.Repository
{
	public class EfDbContext : DbContext
	{
		public EfDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Person> People { get; set; }
		public DbSet<Loan> Loans { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Person>()
				.ToTable("People")
				.HasKey(element => element.Id);

			modelBuilder.Entity<Loan>()
				.ToTable("Ledger")
				.HasKey(element => element.Id);
		}
	}
}