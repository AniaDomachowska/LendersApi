using System.Data.Entity;
using LendersApi.Repository.Model;

namespace LendersApi.Repository
{
	public class EfDbContext : DbContext
	{
		public DbSet<Person> People { get; set; }
		public DbSet<Loan> Ledger { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
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