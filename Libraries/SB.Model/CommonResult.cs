using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Model
{
   public class CommonResult
    {
        public StatusCode Status { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public object Result { get; set; }
    }
    public enum StatusCode
    {
        Sucess = 1,
        Fail = 0,
        Error = 2,
        NoResult = 3
    }
}
