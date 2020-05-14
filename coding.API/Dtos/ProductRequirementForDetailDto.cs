using System;
using System.Collections.Generic;
using coding.API.Dtos.Requirements;
using coding.API.Models.Products.Requirements;

namespace coding.API.Dtos
{
    public class ProductRequirementForDetailDto
    {
        // public Guid ProductId { get; set; }
        // public Guid RequirementId { get; set; }

        // public Requirement Requirement { get; set; }

        public List<RequirementForDetailDto> Requeriments { get; set; }
    }
}