using Microsoft.EntityFrameworkCore;
using OzLotto.Models;
using System;
namespace OzLotto.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

        public DbSet<Ticket> TicketsDb { get; set; } = null!;
    }
}

