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

			modelBuilder.Entity<Person>()
				.Property(element => element.FirstName)
				.HasMaxLength(50)
				.IsRequired();

			modelBuilder.Entity<Person>()
				.Property(element => element.LastName)
				.HasMaxLength(50)
				.IsRequired();

			modelBuilder.Entity<Loan>()
				.ToTable("Loans")
				.HasKey(element => element.Id);

			modelBuilder.Entity<Loan>()
				.Property(element => element.Amount)
				.HasColumnType("decimal(18,2)")
				.IsRequired();

			modelBuilder.Entity<Loan>()
				.Property(element => element.PaidAmount)
				.HasColumnType("decimal(18,2)");

			modelBuilder.Entity<Loan>()
				.Property(element => element.LenderId)
				.IsRequired();

			modelBuilder.Entity<Loan>()
				.Property(element => element.BorrowerId)
				.IsRequired();

			modelBuilder.Entity<Loan>()
				.HasOne(element => element.Borrower)
				.WithMany(element=>element.BorrowerLoans)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Loan>()
				.HasOne(element => element.Lender)
				.WithMany(element => element.LenderLoans)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}