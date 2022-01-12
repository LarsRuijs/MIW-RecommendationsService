using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIW_RecommendationsService.Dal.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }
        public string ImgLink { get; set; }
    }
}