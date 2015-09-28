using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;
using SignalRDemo.Models;
using SignalRDemo.Hubs;
namespace SignalCode.MSMQ
{
    public class MSMQHelper
    {
        public static MessageQueue MQ = new MessageQueue(@".\Private$\MSMQDemo");
        public static void SendMessage<T>(T message)
        {

            System.Messaging.Message myMessage = new System.Messaging.Message();
            myMessage.Body = message;
            myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            MQ.Send(message);
        }

        public static T Receive<T>() where T : class
        {
            MQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                Message msg = MQ.Receive(new TimeSpan(0, 0, 1));
                if (msg != null)
                {
                    return msg.Body as T;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
            }
            return null;

        }

        public static void ThJt()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
               MessageCenter message=Receive<MessageCenter>();
               if (message != null)
               {
                 Users user=PushHub.Users.Where(c => c.LoginName == message.ReceiveUserID).FirstOrDefault();
                 if (user != null)
                 {
                     new PushHub().Send(user.LoginName, message.Content);
                 }
                 else
                 {
                     SendMessage<MessageCenter>(message);
                 }
               }
            }
        }

    }
}