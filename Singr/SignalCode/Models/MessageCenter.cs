
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRDemo.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class MessageCenter
    {

        /// <summary>
        /// 发送人
        /// </summary>
        public string SendUserID { get; set; }


        /// <summary>
        /// 接收人
        /// </summary>
        public string ReceiveUserID { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }



    }
}
