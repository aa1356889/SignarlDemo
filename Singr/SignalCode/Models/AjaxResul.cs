using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRDemo.Models
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }

        public Object Data { get; set; }

        public String Message { get; set; }

        public String ErrCode { get; set; }
    }
}