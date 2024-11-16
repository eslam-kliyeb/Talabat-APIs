using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(ProductBrand))]
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId {  get; set; }
        public ProductType ProductType { get; set; }
    }
}
