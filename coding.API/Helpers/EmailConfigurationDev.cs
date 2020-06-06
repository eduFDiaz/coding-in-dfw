namespace coding.API.Helpers

{
    public class EmailConfigurationDev : IEmailConfiguration
    {

        public  string FromAddress { get; set; }
        public  string FromPassword { get; set; }
        public  string FromName { get; set; }
        public  string SmtpHost { get; set; }
        public  int SmtpPort { get; set; }
        public EmailConfigurationDev()
        {
            FromAddress = "localhost@gmail.com";
            FromPassword = "123456";
            FromName = "Coding in DFW";
            SmtpHost = "localhost";
            SmtpPort = 25;


        }

    }
}
