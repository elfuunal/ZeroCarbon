namespace NeyeTech.ZeroCarbon.Core.Utilities.Settings
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailActivationLink { get; set; }
        public string ForgotPasswordLink { get; set; }
    }
}
