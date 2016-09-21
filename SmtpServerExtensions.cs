using System;

namespace System.Net.Mail
{
    static public class Extensions
    {
        /// <summary>
        /// 取得 SMTP 交易的主機名稱或 IP 位址。
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <returns>SMTP 交易的主機名稱或 IP 位址。</returns>
        public static string GetHostName(this SmtpServer smtpServer)
        {
            return GetSMTPServerAttribute(smtpServer).HostName;
        }

        /// <summary>
        /// 取得 host 上要使用的連接埠。
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <returns>SMTP 交易的主機名稱或 IP 位址。</returns>
        public static int GetPort(this SmtpServer smtpServer)
        {
            return GetSMTPServerAttribute(smtpServer).Port;
        }

        /// <summary>
        /// 查看是否使用 Secure Sockets Layer (SSL) 加密連線。
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <returns></returns>
        static public bool IsEnableSSL(this SmtpServer smtpServer)
        {
            return GetSMTPServerAttribute(smtpServer).EnableSSL;
        }

        /// <summary>
        /// 取得SMTP 伺服器的屬性。
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <returns></returns>
        static private SmtpServerAttribute GetSMTPServerAttribute(SmtpServer smtpServer)
        {
            var type = smtpServer.GetType();

            var members = type.GetMember(smtpServer.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", smtpServer, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(SmtpServerAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format("'{0}.{1}' doesn't have SMTPServerAttribute", type.Name, smtpServer));

            return (SmtpServerAttribute)attributes[0];
        }
    }
}
