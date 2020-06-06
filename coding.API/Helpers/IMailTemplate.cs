using System.Collections.Generic;
using System.Net.Mail;

namespace coding.API.Helpers


{
    public interface IMailTemplate
    {
         string Receiver { get; set; }
         string Subject { get; set; }
         string Body { get; set; }
         List<Attachment> Attachments { get; set; }
    } 
}
