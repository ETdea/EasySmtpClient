namespace System.Net.Mail
{
    /// <summary>
    /// SMTP 伺服器的屬性。
    /// </summary>
    internal class SmtpServerAttribute : Attribute
    {
        private string _hostName;
        private int _port;
        private bool _enableSSL;

        /// <summary>
        /// SMTP 交易的主機名稱或 IP 位址。
        /// </summary>
        public string HostName { get { return _hostName; } }
        /// <summary>
        /// host 上要使用的連接埠。
        /// </summary>
        public int Port { get { return _port; } }
        /// <summary>
        /// 是否使用 Secure Sockets Layer (SSL) 加密連線。
        /// </summary>
        public bool EnableSSL { get { return _enableSSL; } }

        /// <summary>
        /// 指定 SMTP 伺服器的屬性。
        /// </summary>
        /// <param name="hostName">SMTP 交易的主機名稱或 IP 位址。</param>
        /// <param name="port">host 上要使用的連接埠。</param>
        /// <param name="enableSSL">是否使用 Secure Sockets Layer (SSL) 加密連線。</param>
        public SmtpServerAttribute(string hostName, int port = 25, bool enableSSL = true)
        {
            this._hostName = hostName;
            this._port = port;
            this._enableSSL = enableSSL;
        }
    }
}
