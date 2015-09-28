using Microsoft.AspNet.SignalR;
using SignalCode.MSMQ;
using SignalRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SignalRDemo.Hubs
{
    public class PushHub : Hub
    {

        protected static IHubContext ZmHubContext = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
        //暂时用这个存在线用户的链接标示 集群情况下改为redis存
        public static List<Users> Users = new List<Users>();
        /// <summary>
        /// 重写连接事件
        /// </summary>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnConnected()
        {
            //判断连接是否存在  如果不存在则加入在线集合
            if (!Users.Any(c => c.Connectionid == Context.ConnectionId))
            {
                Users.Add(new Users() { Connectionid = Context.ConnectionId, LoginName = HttpContext.Current.User.Identity.Name });
            }
            Groups.Add(Context.ConnectionId, HttpContext.Current.User.Identity.Name);
            return base.OnConnected();
        }
        //重新连接
        public override System.Threading.Tasks.Task OnReconnected()
        {
            Groups.Add(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnReconnected();
        }

        /// 断开连接的事件
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Users.Where(u => u.Connectionid == Context.ConnectionId).FirstOrDefault();

            //判断用户是否存在,存在则删除
            if (user != null)
            {
                //删除用户
                Users.Remove(user);

            }
            Groups.Remove(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnDisconnected(stopCalled);
        }


        /// <summary>
        /// 给指定用户发送信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="message"></param>
        public void Send(string loginName, string message)
        {
            var user = Users.SingleOrDefault(c => c.LoginName == loginName);
            //表示用户离线状态
            if (user == null)
            {
                MessageCenter center=new MessageCenter(){Content=message,ReceiveUserID=loginName,SendUserID=HttpContext.Current.User.Identity.Name};
                //仍回队列
                MSMQHelper.SendMessage<MessageCenter>(center);
            }
            else
            {
                ZmHubContext.Clients.Groups(new string[] { loginName }).notice(message);
            }
        }
    }
}