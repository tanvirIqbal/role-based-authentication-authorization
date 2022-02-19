using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetAuth.Data
{
    public class ResponseModel
    {
        public ResponseModel(ResponseCode code, string message, object dataSet)
        {
            Code = code;
            Message = message;
            DataSet = dataSet;
        }
        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public object DataSet { get; set; }
    }
}