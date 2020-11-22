using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Dtos
{
    [ExcludeFromCodeCoverage]
    public class CreateFeatureSkillDto
    {

        public string[] Icons { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        
        
    }
}