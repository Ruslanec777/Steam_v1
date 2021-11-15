using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class ReturnedData
    {
        public string ReturnedString { get; set; }
        public ExecutionStatusCode ExecutionStatusCode { get; set; }

        public bool Issuccessfull
        {
            get { return ExecutionStatusCode == ExecutionStatusCode.CorrectCompletion; }
        }


    }
}
