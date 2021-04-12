using System;

namespace ESE.Porchase.API.Models
{
    public class ItemProductDTO
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
