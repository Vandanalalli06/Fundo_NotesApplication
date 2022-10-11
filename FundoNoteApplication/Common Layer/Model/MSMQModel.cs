using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\Tokens";//setting queue path we want to store msg.
            if (!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
            }
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });//it is used to convert msg into string and communicate with Application.
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;//Through this method we will send mail
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "FundoNote App Reset Link";
            string body = token;

            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,//port num for gmail is 587(standard secure SMTP port)
                Credentials = new NetworkCredential("dadivandana111@gmail.com", "iclzgygqkvhnjirt"),
                EnableSsl = true,
            };
            SMTP.Send("dadivandana111@gmail.com", "dadivandana111@gmail.com", subject, body);
            messageQ.BeginReceive();
        }
    }
}

