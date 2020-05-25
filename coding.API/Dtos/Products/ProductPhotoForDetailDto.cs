using System;

namespace coding.API.Dtos.Products
{
    public class ProductPhotoForDetailDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}