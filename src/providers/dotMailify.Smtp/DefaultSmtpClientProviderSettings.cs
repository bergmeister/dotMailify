using System.Configuration;
using System.Net.Configuration;

namespace dotMailify.Smtp
{
    public sealed class DefaultSmtpClientProviderSettings : ISmtpClientProviderSettings
    {
        public bool EnableDelivery { get; } = false;
        public string Host { get; private set; }
        public int Port { get; private set; } = 25;
        public bool EnableSsl { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        private void ConfigureFromMailAppSettings()
        {
            var settings = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
            if (settings != null)
            {
                EnableSsl = settings?.Network?.EnableSsl ?? false;
                Host = settings?.Network?.Host;
                Username = settings?.Network?.UserName;
                Password = settings?.Network?.Password;
                Port = settings?.Network?.Port ?? 25;
            }
        }

        public DefaultSmtpClientProviderSettings()
        {
            ConfigureFromMailAppSettings();
        }
    }
}