using API.Contexts;
using API.Handlers;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace API.Repositories.Data;

public class AccountRepositories : GeneralRepository<MyContext, Account, string>
{
	private readonly MyContext _context;
	public AccountRepositories(MyContext context) : base(context)
	{
		_context = context;
	}

	public int Register(RegisterVM register)
	{
		var duplicate = _context.Accounts.Join(_context.Employees, a => a.NIK, e => e.NIK, (a, e) =>
		new RegisterVM
		{
			NIK = a.NIK,
			Email = e.Email,
			Username = a.Username
		});

		var check = duplicate.Where(s => s.NIK == register.NIK).ToList();
		if (check.Any())
		{
			return 0; // NIK SUDAH ADA
		}

		check = duplicate.Where(s => s.Email == register.Email).ToList();
		if (check.Any())
		{
			return 1; // EMAIL SUDAH ADA
		}

		check = duplicate.Where(s => s.Username == register.Username).ToList();
		if (check.Any())
		{
			return 2; // USERNAME SUDAH ADA
		}

		Participant participant = new Participant()
		{
			NIK = register.NIK,
			Batch = register.Batch,
			Status_MCC = (Status_MCC)1,
		};
		_context.Participants.Add(participant);

		Employee employee = new Employee()
		{
			NIK = register.NIK,
			Name = register.Fullname,
			Email = register.Email,
			Position = register.Position,
			ClassID = register.ClassID,
		};
		_context.Employees.Add(employee);

		Account account = new Account()
		{
			NIK= register.NIK,
			Username = register.Username,
			Password = Hashing.HashPassword(register.Password),
			Role = (Role)1,
			IsValid = true,
		};
		_context.Accounts.Add(account);
		_context.SaveChanges();

		return 3;
	}

	public int Login(LoginVM login)
	{
		var result = _context.Accounts.Join(_context.Employees, a => a.NIK, e => e.NIK, (a, e) =>
		new LoginVM
		{
			Email_Username = e.Email,
			Password = a.Password
		}).SingleOrDefault(c => c.Email_Username == login.Email_Username); // Cek email
		if (result == null)
		{
			result = _context.Accounts.Join(_context.Employees, a => a.NIK, e => e.NIK, (a, e) =>
		new LoginVM
		{
			Email_Username = a.Username,
			Password = a.Password
		}).SingleOrDefault(c => c.Email_Username == login.Email_Username); // Cek Username

			if (result == null)
			{
				return 0; //email atau username tidak terdaftar
			}
		}
		if (!Hashing.ValidatePassword(login.Password, result.Password))
		{
			return 1; // Password Salah
		}
		return 2; // Email dan Password Benar
	}

	public string UserRoles(string email_username)
	{
		var getNIK = "";
		try
		{
			getNIK = _context.Employees.SingleOrDefault(e => e.Email == email_username).NIK;
		}
		catch 
		{
			getNIK = _context.Accounts.SingleOrDefault(e => e.Username == email_username).NIK;
		}
	
		var getRoles = Enum.GetName(_context.Accounts.SingleOrDefault(ac => ac.NIK == getNIK).Role);

		return getRoles;
	}

	public string GetNIK(string email_username)
	{
		var getNIK = "";
		try
		{
			getNIK = _context.Employees.SingleOrDefault(e => e.Email == email_username).NIK;
		}
		catch
		{
			getNIK = _context.Accounts.SingleOrDefault(e => e.Username == email_username).NIK;
		}
		return getNIK;
	}

}

