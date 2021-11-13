using SteamClientLab.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamClientLab.Model
{
    public class MenuException:Exception
    {


        public MenuExceptions ErrorCode { get; set; }

        public MenuException (string menu ,MenuExceptions errorCode) : base(menu)
        {
            ErrorCode = errorCode;
        }

        
    }
}
