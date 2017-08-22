using Microsoft.EntityFrameworkCore;

namespace niner.Models
{
  public class NinerContext : DbContext
  {
    public NinerContext(DbContextOptions<NinerContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
  }
}