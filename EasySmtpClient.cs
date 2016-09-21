using Microsoft.AspNet.Identity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Mail
{
    /// <summary>
    /// 摘要: 
    ///    允許應用程式使用 Simple Mail Transfer Protocol (SMTP) 傳送電子郵件。
    ///    繼承自System.Net.Mail.SmtpClient。
    /// </summary>
    public class EasySmtpClient : System.Net.Mail.SmtpClient, IIdentityMessageService
    {
        protected MailMessage _mailMessage;
        public MailMessage MailMessage
        {
            get
            {
                if (_mailMessage == null)
                {
                    _mailMessage = new MailMessage();
                }

                return _mailMessage;
            }
            set
            {
                _mailMessage = value;
            }
        }

        static public SmtpConfiguration GetConfiguration(System.Configuration.Configuration config = null)
        {
            return new SmtpConfiguration(config);
        }

        #region Constructors
        /// <summary>
        /// 使用web.config
        /// http://msdn.microsoft.com/en-us/library/w355a94k
        /// </summary>
        public EasySmtpClient() : base() { }
        /// <summary>
        /// 初始化 System.Net.Mail.SmtpClient 類別的新執行個體，該類別使用指定的 SMTP 伺服器來傳送電子郵件。
        /// </summary>
        /// <param name="host">用於 SMTP 交易的主機電腦名稱或 IP 位址。</param>
        /// <param name="enableSsl">是否使用 Secure Sockets Layer (SSL) 加密連線。</param>
        public EasySmtpClient(string host, bool enableSsl) : base(host) { EnableSsl = enableSsl; }
        /// <summary>
        /// 初始化 System.Net.Mail.SmtpClient 類別的新執行個體，該類別使用指定的 SMTP 伺服器和連接埠來傳送電子郵件。
        /// </summary>
        /// <param name="host">System.String，包含用於 SMTP 交易的主機名稱或 IP 位址。</param>
        /// <param name="port">host 上要使用的連接埠。</param>
        /// <param name="enableSsl">是否使用 Secure Sockets Layer (SSL) 加密連線。</param>
        public EasySmtpClient(string host, int port, bool enableSsl) : base(host, port) { EnableSsl = enableSsl; }
        /// <summary>
        /// 初始化 System.Net.Mail.SmtpClient 類別的新執行個體，該類別使用指定的 SMTP 伺服器和連接埠來傳送電子郵件。
        /// </summary>
        /// <param name="smtpServer"></param>
        public EasySmtpClient(SmtpServer smtpServer) : this(smtpServer.GetHostName(), smtpServer.GetPort(), smtpServer.IsEnableSSL()) { }
        #endregion

        /// <summary>
        /// 使用指定的使用者名稱和密碼，設定用來驗證寄件者的認證。
        /// </summary>
        /// <param name="userName">與認證相關的使用者名稱。</param>
        /// <param name="password">與認證相關的使用者名稱的密碼。</param>
        public void SetCredentials(string userName, string password)
        {
            Credentials = new NetworkCredential(userName, password);
        }

        /// <summary>
        /// 設定電子郵件訊息的寄件者地址。
        /// </summary>
        /// <param name="address">電子郵件地址。</param>
        public void SetMailMessageForm(string address)
        {
            MailMessage.From = new MailAddress(address);
        }
        /// <summary>
        /// 設定電子郵件訊息的寄件者地址。
        /// </summary>
        /// <param name="address">電子郵件地址。</param>
        /// <param name="displayName">與 address 關聯的顯示名稱。</param>
        public void SetMailMessageForm(string address, string displayName)
        {
            MailMessage.From = new MailAddress(address, displayName);
        }
        /// <summary>
        /// 設定電子郵件訊息的寄件者地址。
        /// </summary>
        /// <param name="address"></param>
        /// <param name="displayName">與 address 關聯的顯示名稱。</param>
        /// <param name="displayNameEncoding">定義用於 displayName 的字元集。</param>
        public void SetMailMessageForm(string address, string displayName, Encoding displayNameEncoding)
        {
            MailMessage.From = new MailAddress(address, displayName, displayNameEncoding);
        }
        /// <summary>
        /// 設定訊息主體。
        /// </summary>
        /// <param name="body">訊息主體的複合格式字串。/param>
        /// <param name="args">參數陣列。</param>
        public void SetMailMessageBody(string body, params object[] args)
        {
            SetMailMessageBody(body, false, args);
        }
        /// <summary>
        /// 設定訊息主體。
        /// </summary>
        /// <param name="body">訊息主體的複合格式字串。</param>
        /// <param name="isBodyHtml">指出郵件訊息主體是否採用 Html 格式。</param>
        /// <param name="args">參數陣列。</param>
        public void SetMailMessageBody(string body, bool isBodyHtml, params object[] args)
        {
            if (args != null && args.Length != 0)
            {
                MailMessage.Body = String.Format(body, args);
            }
            else
            {
                MailMessage.Body = body;
            }

            MailMessage.IsBodyHtml = isBodyHtml;
        }
        /// <summary>
        /// 設定電子郵件訊息的收件者地址。
        /// </summary>
        /// <param name="emails">電子郵件地址。</param>
        public void SetMailMessageTo(params string[] emails)
        {
            MailMessage.To.Clear();
            emails.ToList().ForEach(p => MailMessage.To.Add(p));
        }
        /// <summary>
        /// 設定電子郵件訊息的副本 (CC) 收件者。
        /// </summary>
        /// <param name="emails">電子郵件地址。</param>
        public void SetMailMessageCC(params string[] emails)
        {
            MailMessage.To.Clear();
            emails.ToList().ForEach(p => MailMessage.CC.Add(p));
        }
        /// <summary>
        /// 電子郵件訊息的密件副本 (BCC) 收件者。
        /// </summary>
        /// <param name="emails">電子郵件地址。</param>
        public void SetMailMessageBcc(params string[] emails)
        {
            MailMessage.To.Clear();
            emails.ToList().ForEach(p => MailMessage.Bcc.Add(p));
        }

        public virtual async Task SendAsync(IdentityMessage message)
        {
            if (!string.IsNullOrEmpty(message.Destination))
            {
                var to = message.Destination.Split(',');
                SetMailMessageTo(to);
            }

            MailMessage.Subject = message.Subject;
            MailMessage.Body = message.Body;
            MailMessage.IsBodyHtml = true;

            await base.SendMailAsync(MailMessage);
        }
    }
}
