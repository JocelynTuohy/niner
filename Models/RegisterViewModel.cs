using System.ComponentModel.DataAnnotations;

namespace niner.Models
{
  public class RegisterViewModel : BaseEntity
  {
    [Required]
    public string Alias { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; }
  }
}