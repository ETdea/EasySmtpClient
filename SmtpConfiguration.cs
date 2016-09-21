using System.Configuration;
using System.Net.Configuration;
using System.Web;
using System.Web.Configuration;

namespace System.Net.Mail
{
    public class SmtpConfiguration
    {
        private System.Configuration.Configuration config;
        private MailSettingsSectionGroup sectionGroup;

        public SmtpSection Settings
        {
            get
            {
                if (sectionGroup == null)
                {
                    sectionGroup = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                }

                return sectionGroup.Smtp;
            }
        }

        public SmtpConfiguration(System.Configuration.Configuration config = null)
        {
            if (config == null)
            {
                config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            }

            this.config = config;
        }

        public void save()
        {
            config.Save();
        }
    }
}
