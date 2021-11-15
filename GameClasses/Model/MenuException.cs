using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class MenuException : Exception
    {
        public MenuExceptions ErrorCode { get; set; }

        public MenuException(MenuExceptions errorCode, string menu = null) : base(menu)
        {
            ErrorCode = errorCode;
        }


    }
}
