
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
 
namespace email
{

    /// <summary>
    /// 软件的邮箱类，用于发送邮箱数据
    /// </summary>
    public class Emailhelp
    {
        public static  string SendMailUseZj(string EmailAd,string EmailSubject,string EmailBody)

        {

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            //msg.To.Add("stoney_xu@highrock.com.cn");
            msg.To.Add(EmailAd);

            //msg.To.Add(b@b.com);

            /*   

            * msg.To.Add("b@b.com");   

            * msg.To.Add("b@b.com");   

            * msg.To.Add("b@b.com");可以发送给多人   

            */

            //msg.CC.Add("c@c.com");

            /*   

            * msg.CC.Add("c@c.com");   

            * msg.CC.Add("c@c.com");可以抄送给多人   

            */

            msg.From = new MailAddress("stoney@hexaoutdoor.com", "Stoney", System.Text.Encoding.UTF8);

            /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/

            //msg.Subject = "这是测试邮件";//邮件标题    
            msg.Subject = EmailSubject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码    

            //msg.Body = "邮件内容";//邮件内容    
            msg.Body = EmailBody;
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码    

            msg.IsBodyHtml = false;//是否是HTML邮件    

            msg.Priority = MailPriority.High;//邮件优先级    



            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential("stoney@hexaoutdoor.com", "VyursTs4dMrY4JZ4");

            //在71info.com注册的邮箱和密码    

            client.Host = "smtp.qiye.163.com";

            object userState = msg;

            try

            {

                client.SendAsync(msg, userState);

                //简单一点儿可以client.Send(msg);    
                return "发送成功";
                //MessageBox.Show("发送成功");

            }

            catch (System.Net.Mail.SmtpException ex)

            {
                return ex.Message;
                //MessageBox.Show(ex.Message, "发送邮件出错");

            }

        }

























        public static Emailhelp MailSystem163 = new Emailhelp(
           mail =>
           {
               mail.Host = "smtp.163.com";//使用163的SMTP服务器发送邮件
                                          //mail.UseDefaultCredentials = true;//在winform平台使用默认值
               mail.EnableSsl = true;
               mail.UseDefaultCredentials = false;//在.framework或mvc下使用这个
               mail.Port = 25;//端口号
               mail.DeliveryMethod = SmtpDeliveryMethod.Network;
               mail.Credentials = new System.Net.NetworkCredential("邮件发送地址（自己的邮件号）如1234656@qq.com", "刚才获取的授权码");
           },
           "邮件发送地址（自己的邮件号）如1234656@qq.com",
           "邮件发送地址（自己的邮件号）如1234656@qq.com"
       );
        /// <summary>
        /// 系统连续发送失败的次数，为了不影响系统，连续三次失败就禁止发送
        /// </summary>
        private static long SoftMailSendFailedCount { get; set; } = 0L;


        /// <summary>
        /// 系统的邮件发送客户端
        /// </summary>
        private SmtpClient smtpClient { get; set; }

        /// <summary>
        /// 发送邮件的地址
        /// </summary>
        private string MailFromAddress { get; set; } = "";


        /// <summary>
        /// 邮件发送的地址
        /// </summary>
        public string MailSendAddress { get; set; } = "";


        /// <summary>
        /// 实例化一个邮箱发送类，需要指定初始化信息
        /// </summary>
        /// <param name="mailIni">初始化的方法</param>
        /// <param name="addr_From">发送地址，应该和账户匹配</param>
        /// <param name="addr_to">邮件接收地址</param>
        /// <remarks>
        /// </remarks>
        public Emailhelp(Action<SmtpClient> mailIni, string addr_From = "", string addr_to = "")
        {
            smtpClient = new SmtpClient();
            mailIni(smtpClient);
            MailFromAddress = addr_From;
            MailSendAddress = addr_to;
        }

        private string GetExceptionMail(Exception ex)
        {
            return ex.Message;
        }

        /// <summary>
        /// 发生BUG至邮件地址，需要提前指定发送地址，否则失败
        /// </summary>
        /// <param name="ex">异常的BUG，同样试用兼容类型</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(Exception ex)
        {
            return SendMail(ex, "");
        }

        /// <summary>
        /// 发送邮件至地址，需要提前指定发送地址，否则失败
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(string subject, string body)
        {
            return SendMail(MailSendAddress, subject, body);
        }

        /// <summary>
        /// 发送邮件至地址，需要提前指定发送地址，否则失败
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isHtml">是否是html格式化文本</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(string subject, string body, bool isHtml)
        {
            return SendMail(MailSendAddress, subject, body, isHtml);
        }

        /// <summary>
        /// 发生BUG至邮件地址，需要提前指定发送地址，否则失败
        /// </summary>
        /// <param name="ex">异常的BUG，同样试用兼容类型</param>
        /// <param name="addtion">额外信息</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(Exception ex, string addtion)
        {
            if (string.IsNullOrEmpty(MailSendAddress))
            {
                return false;
            }
            return SendMail(MailSendAddress, "", string.IsNullOrEmpty(addtion) ? GetExceptionMail(ex) : ("User：" + addtion + Environment.NewLine + GetExceptionMail(ex)));
        }

        /// <summary>
        /// 发送邮件的方法，需要指定接收地址，主题及内容
        /// </summary>
        /// <param name="addr_to">接收地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(string addr_to, string subject, string body)
        {
            return SendMail(addr_to, subject, body, isHtml: false);
        }

        /// <summary>
        /// 发送邮件的方法，默认发送别名，优先级，是否HTML
        /// </summary>
        /// <param name="addr_to">接收地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isHtml">是否是html格式的内容</param>
        /// <returns>是否发送成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(string addr_to, string subject, string body, bool isHtml)
        {
            return SendMail(MailFromAddress, "", new string[1] { addr_to }, subject, body, MailPriority.Normal, isHtml);
        }

        /// <summary>
        /// 发送邮件的方法，需要提供完整的参数信息
        /// </summary>
        /// <param name="addr_from">发送地址</param>
        /// <param name="name">发送别名</param>
        /// <param name="addr_to">接收地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="priority">优先级</param>
        /// <param name="isHtml">邮件内容是否是HTML语言</param>
        /// <returns>发生是否成功，内容不正确会被视为垃圾邮件</returns>
        public bool SendMail(string addr_from, string name, string[] addr_to, string subject, string body, MailPriority priority, bool isHtml)
        {
            if (SoftMailSendFailedCount > 10)
            {
                SoftMailSendFailedCount++;
                return true;
            }
            MailMessage mailMessage = new MailMessage();
#pragma warning disable CS0168 // 声明了变量，但从未使用过
            try
            {
                mailMessage.From = new MailAddress(addr_from, name, Encoding.UTF8);
                foreach (string addresses in addr_to)
                {
                    mailMessage.To.Add(addresses);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                MailMessage mailMessage2 = mailMessage;
                mailMessage2.Body = mailMessage2.Body + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Priority = priority;
                mailMessage.IsBodyHtml = isHtml;
                smtpClient.Send(mailMessage);
                SoftMailSendFailedCount = 0L;
                return true;
            }
            catch (Exception ex)
            {
                SoftMailSendFailedCount++;
                return false;
            }
#pragma warning restore CS0168 // 声明了变量，但从未使用过
        }
    }
}