using System;
using System.Collections.Generic;

namespace niner.Models
{
  public class User : BaseEntity
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Alias { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; set; }
    public List<Like> Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User()
    {
      Posts = new List<Post>();
      Likes = new List<Like>();
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}