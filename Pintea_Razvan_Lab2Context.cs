using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

public class Pintea_Razvan_Lab2Context(DbContextOptions<Pintea_Razvan_Lab2Context> options) : DbContext(options)
{
    public DbSet<Pintea_Razvan_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Pintea_Razvan_Lab2.Models.Publishers> Publishers { get; set; } = default!;
    
public DbSet<Authors> Authors { get; set; } = default!;

}
