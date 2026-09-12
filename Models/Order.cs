using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SouvenirShop.Models{

    public class Order
    {
        public int? id {get; set; }
        public int? User_id {get; set; }
        public int[]? Souvenirs_id {get; set; }

        public string? Order_status {get; set; }
    }
}