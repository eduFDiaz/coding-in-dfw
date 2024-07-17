

using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models.FeatureSkills
{
    [ExcludeFromCodeCoverage]
    public class FeatureSkill : BaseEntity
    {
        public string[] Icons { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }


    }
}