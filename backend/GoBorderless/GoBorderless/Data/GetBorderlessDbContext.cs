using Microsoft.EntityFrameworkCore;

namespace GoBorderless.Data;

public class GetBorderlessDbContext(DbContextOptions<GetBorderlessDbContext> options) : DbContext(options)
{
    public DbSet<SampleTable> Sample { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
}