using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Model;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Display(Name="Display order")]
    [Range(1, 100, ErrorMessage = "Display order has to be between 1 and 100.")]
    public int DisplayOrder { get; set; }
}
