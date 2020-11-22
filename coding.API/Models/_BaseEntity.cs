using System;
using System.Diagnostics.CodeAnalysis;

namespace coding.API.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }
    }
}