using System.ComponentModel.DataAnnotations.Schema;

namespace SouvenirShop.Models{

public class Souvenir{
    public int? id {get; set; }
    public string? name {get; set; }
    public decimal? Price {get; set; }
    public string? Description {get; set; }
    public string? ImageUrl {get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>();

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
}