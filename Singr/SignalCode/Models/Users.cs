using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDemo.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
      [Serializable]
    public class Users
    {
        /// <summary>
        /// 用户对应的长连接id
        /// </summary>
        public string Connectionid { get; set; }
        public string LoginName { get; set; }

        public string Pwd { get; set; }
    }
}