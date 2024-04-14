using Microsoft.EntityFrameworkCore;
using NeoEditAPI.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; }
}
