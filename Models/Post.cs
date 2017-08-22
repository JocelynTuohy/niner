using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace niner.Models
{
  public class Post : BaseEntity
  {
    public int PostId { get; set; }
    [Required]
    public string Content { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Like> Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Post()
    {
      Likes = new List<Like>();
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}