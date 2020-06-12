using System;
namespace coding.API.Helpers

{
    public class EmailConfigurationProd : IEmailConfiguration
    {

        public string FromAddress { get; set; }
        public string FromPassword { get; set; }
        public string FromName { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }

        public EmailConfigurationProd()
        {
            Console.WriteLine("Configuring mail");
            FromAddress = "dcruzbv1990@gmail.com";
            FromPassword = "Wundstarrkrampf";
            FromName = "Coding in DFW";
            SmtpHost = "smtp.gmail.com";
            SmtpPort = 587;


        }

    }
}
