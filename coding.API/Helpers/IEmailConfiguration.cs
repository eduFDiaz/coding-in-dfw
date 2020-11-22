using System.Diagnostics.CodeAnalysis;

namespace coding.API.Helpers

{
    
    public interface IEmailConfiguration
    {
         string FromAddress { get; set; }
         string FromPassword { get; set; }
         string FromName { get; set; }
         string SmtpHost { get; set; }
         int SmtpPort { get; set; }
    }

}