using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models {
  public class Contact {

    [Required]
    public string Name { get; set; }

    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    [MinLength (10)]
    [Required]
    public string Message { get; set; }
  }
}