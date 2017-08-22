using System;
using System.Collections.Generic;

namespace niner.Models
{
  public class Like : BaseEntity
  {
    public int LikeId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Like()
    {
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}