using System;
using System.Diagnostics.CodeAnalysis;
using coding.API.Models.Products;

namespace coding.API.Models.Photos
{
    [ExcludeFromCodeCoverage]
    public class ProductPhoto  : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        // This is how is defined that a photo belongs to an user
        // see user model (public ICollection<Photo> Photos { get; set; })
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public string  PublicId { get; set; }
    }
}