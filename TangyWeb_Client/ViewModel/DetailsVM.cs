
using System.ComponentModel.DataAnnotations;
using Tangy_Models;

namespace TangyWeb_Client.ViewModel;

public class DetailsVM
{
    public DetailsVM() // default values of a product
    {
        ProductPrice = new();
        Count = 1;
    }
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
    public int Count { get; set; }
    [Required]
    public int SelectedProductPriceId { get; set; }
    public ProductPriceDTO ProductPrice { get; set; }
}

// We donot create this model in Models project because that is ment for DTOs that will talk with API Endpoints
