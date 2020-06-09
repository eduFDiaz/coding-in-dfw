using System;
using System.Collections.Generic;

namespace coding.API.Models.FeatureSkills
{
    public class FeatureSkill : BaseEntity
    {
        public string[] Icons { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }


    }
}