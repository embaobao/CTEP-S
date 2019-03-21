using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace CTEP.Models
{

    public class EMail
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string FilePath { get; set; }
        public string RandomNumber { get; set; }
        public string UserName { get; set; }
        public string From { get; set; }
        public string ID { get; set; }
        public string PassWord { get; set; }
        public Dictionary<string, string> ClientUserList { get; set; }
        public List<SmtpClient> ClientsList { get; set; }
        public SmtpClient SendClient { get; set; }
        public MailMessage Message { get; set; }

        [Obsolete]
        public EMail(string to)
        {

            //添加发送邮件客户端账户和授权码
            string pw = "woshizhumeng0";
            this.ClientUserList = new Dictionary<string, string>();
            this.ClientUserList.Add("1132067567@qq.com", "frznaobvoegvhffe");
            this.ClientUserList.Add("907022053@qq.com", "uuvwkpcobtljbbeh");
            this.ClientUserList.Add("embaobao@126.com", pw);
            this.ClientUserList.Add("15515365219@163.com", pw);
            this.ClientUserList.Add("chihuoxingdebaobao@163.com", pw);



            Random Arandom = new Random();
            this.UserName = "BaoBao大人";
            this.RandomNumber = ((char)Arandom.Next(65, 90)).ToString()
                                + ((char)Arandom.Next(65, 90)).ToString()
                                + Arandom.Next(1000, 9999).ToString()
                                + ((char)Arandom.Next(65, 90)).ToString()
                                + ((char)Arandom.Next(97, 122)).ToString();
            this.To = to;
            //this.ID = "chihuoxingdebaobao@163.com";
            //this.PassWord = "woshizhumeng0";
            this.From = this.ID;
            this.FilePath = string.Empty;
            this.Title = "E.M.B账户激活|测试邮件";
            this.Body = string.Format
            ("尊敬的{0}！<br/>你的账户激活验证码: {1}", this.UserName, this.RandomNumber);

            this.SendClient = new SmtpClient();
            this.Message = new MailMessage();
            SetSendClient(); // 同时设置Form host ID账户
                                                   //开始对邮件信息内容进行设置
                                                   //发送者邮箱地址 可以其他邮箱
            this.Message.From = new MailAddress(this.From);
            //默认回复邮箱地址
            Message.ReplyTo = new MailAddress(this.From);
            //清除接收者列表
            this.Message.To.Clear();
            //添加接收者邮箱地址到接收邮件列表
            this.Message.To.Add(new MailAddress(this.To));
            //邮件的主题
            this.Message.Subject = this.Title;
            //邮件的内容是否是html格式
            this.Message.IsBodyHtml = true;
            //邮件的优先级
            this.Message.Priority = MailPriority.High;
            //邮件的内容编码
            this.Message.BodyEncoding = Encoding.GetEncoding(936);
            //邮件的内容
            this.Message.Body = this.Body;
            //添加附件地址
            if (!string.IsNullOrEmpty(this.FilePath))
            {
                this.Message.Attachments.Add(new Attachment(this.FilePath)); //System.Net.Mime.MediaTypeNames.Text
            }

            //设置客户端发送邮件的延时  单位：毫秒
            this.SendClient.Timeout = 2000;
            //设置递交方法 使用的远程SMTP服務器。
            this.SendClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //设置客户端 端口号 SMTP默认25 465
            this.SendClient.Port = 25;
            //设置是否ssl协议
            this.SendClient.EnableSsl = true;
            ///设置不和请求一块发送。
            this.SendClient.UseDefaultCredentials = false;
            //创建连接身份验证  如果是163 账户不用加@163.com
            this.SendClient.Credentials = new NetworkCredential(this.ID, this.PassWord);
        }

        [Obsolete]
        public bool Send()
        {
            try
            {
                //发送者邮箱地址 可以其他邮箱
                this.Message.From = new MailAddress(this.From);
                //默认回复邮箱地址
                Message.ReplyTo = new MailAddress(this.From);
                //邮件的主题
                this.Message.Subject = this.Title;
                //邮件的内容
                this.Message.Body = this.Body;
                //创建连接身份验证  如果是163 账户不用加@163.com
                this.SendClient.Credentials = new NetworkCredential(this.ID, this.PassWord);
                // 客户端发送 邮件信息对象
                this.SendClient.Send(this.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                SendClient.Dispose();
            }

          
            //                Thread.Sleep(1000);
            return true;
        }

        [Obsolete]
        public bool Send(Users user)
        {
            try
            {
                 
                //发送者邮箱地址 可以其他邮箱
                this.Message.From = new MailAddress(this.From);
                //默认回复邮箱地址
                Message.ReplyTo = new MailAddress(this.From);
                //邮件的主题
                this.Message.Subject = this.Title;
                //邮件的内容
                this.Message.Body =string.Format("尊敬的{0}您好!<br/>请您点击以下链接 -》<a href=\"{1}\" target=\"_blank\">激活您的账户邮箱</a>"
                    ,user.email
                    ,Tools.BaseUrl+ "User/AccountActive?user="+Tools.ToBase64<Users>(user));
                //创建连接身份验证  如果是163 账户不用加@163.com
                this.SendClient.Credentials = new NetworkCredential(this.ID, this.PassWord);
                // 客户端发送 邮件信息对象
                this.SendClient.Send(this.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                SendClient.Dispose();
            }


            //                Thread.Sleep(1000);
            return true;
        }

        public void SetSendClient()
        {

            int max = this.ClientUserList.Keys.Count;
            Random random = new Random();
            int num = random.Next(0, max);
            string id = ClientUserList.Keys.ToArray()[num];
            string pw = ClientUserList.Values.ToArray()[num];
            this.From = id;
            this.ID = id;
            this.PassWord = pw;
            this.SendClient.Host = "smtp.qq.com";
            if (id.Contains("@163.com"))
            {
                this.ID = id.Replace("@163.com", "");
                //设置邮箱服务器地址
                this.SendClient.Host = "smtp.163.com";
            }
            else if (id.Contains("@126.com"))
            {
                this.ID = id.Replace("@126.com", "");
                //设置邮箱服务器地址
                this.SendClient.Host = "smtp.126.com";
            }
        }

    }


    //public static void sendMail()
    //{
    //    MailMessage mail = new MailMessage();
    //    //开始对邮件信息内容进行设置
    //    //发送者邮箱地址 可以其他邮箱
    //    mail.From = new MailAddress("chihuoxingdebaobao@163.com");
    //    //默认回复邮箱地址
    //    mail.ReplyTo = new MailAddress("chihuoxingdebaobao@163.com");
    //    //清除接收者列表
    //    mail.To.Clear();
    //    //添加接收者邮箱地址到接收邮件列表
    //    mail.To.Add(new MailAddress("907022053@qq.com"));
    //    //邮件的主题
    //    mail.Subject = "TEST";
    //    //邮件的内容是否是html格式
    //    mail.IsBodyHtml = true;
    //    //邮件的优先级
    //    mail.Priority = MailPriority.High;
    //    //邮件的内容编码
    //    mail.BodyEncoding = Encoding.GetEncoding(936);
    //    //邮件的内容
    //    mail.Body = " <BR/><HR/>TSET 邮件主体内容";
    //    //添加附件地址
    //    //            mail.Attachments.Add(new Attachment(""));//System.Net.Mime.MediaTypeNames.Text


    //    SmtpClient client = new SmtpClient();
    //    //设置邮箱服务器地址
    //    client.Host = "smtp.163.com";
    //    //设置客户端发送邮件的延时  单位：毫秒
    //    client.Timeout = 3000;

    //    //设置递交方法 使用的远程SMTP服務器。
    //    client.DeliveryMethod = SmtpDeliveryMethod.Network;

    //    //设置客户端 端口号 SMTP默认25 465
    //    client.Port = 25;
    //    //设置是否ssl协议
    //    client.EnableSsl = true;
    //    ///设置不和请求一块发送。
    //    client.UseDefaultCredentials = false;
    //    //创建连接身份验证  如果是163 账户不用加@163.com
    //    client.Credentials = new NetworkCredential("chihuoxingdebaobao", "woshizhumeng0");

    //    // 客户端发送 邮件信息对象
    //    client.Send(mail);
    //    Console.WriteLine("发送成功！");
    //}

}




