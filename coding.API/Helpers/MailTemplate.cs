using System.Collections.Generic;
using System.Net.Mail;

namespace coding.API.Helpers


{
    public class MailTemplate: IMailTemplate
    {
        public  string Receiver { get; set; }
        public  string Subject { get; set; }
        public  string Body { get; set; }
        public  List<Attachment> Attachments { get; set; }

        public MailTemplate(string Receiver, string Body) {
            this.Receiver = Receiver;
            Subject = "Review Invitation from Coding in DFW";
            this.Body = Body;
            Attachments = null;
        }


    }
}