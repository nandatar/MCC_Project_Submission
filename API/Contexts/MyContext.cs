using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;

namespace API.Contexts;

public class MyContext : DbContext
{
	public MyContext(DbContextOptions<MyContext> options) : base(options)
	{

	}

	// Introduces the Model to the Database that eventually becomes an Entity
	public DbSet<Account> Accounts { get; set; }
	public DbSet<ClassMCC> ClassMCC { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<History> Histories { get; set; }
	public DbSet<Participant> Participants { get; set; }
	public DbSet<Project> Projects { get; set; }
	public DbSet<Status> Status { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure Unique Constraint
		// FAQ : Kenapa gk pake anotasi? gk bisa dan ribet :))
		//      modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Phone);
		//modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Email);

		// Configure PK as FK 
		// FAQ : Kenapa gk pake anotasi? Karena Data Anotation gk support kalau ada PK sebagai FK juga
		// One Account to One Employee
		modelBuilder.Entity<Employee>()
			.HasOne(a => a.Account)
			.WithOne(e => e.Employee)
			.HasForeignKey<Account>(a => a.NIK);

		// One Account to One Profiling
		modelBuilder.Entity<Participant>()
			.HasOne(a => a.Employee)
			.WithOne(e => e.Participant)
			.HasForeignKey<Employee>(a => a.NIK);

	}
}
