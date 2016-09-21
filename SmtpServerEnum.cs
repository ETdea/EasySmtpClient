namespace System.Net.Mail
{
    /// <summary>
    /// 指定 SMTP 伺服器。
    /// </summary>
    public enum SmtpServer
    {
        [SmtpServer("smtp.gmail.com", 587)]
        Gmail = 1,

        [SmtpServer("smtp-mail.outlook.com", 587)]
        Outlook = 2,
    }
}
