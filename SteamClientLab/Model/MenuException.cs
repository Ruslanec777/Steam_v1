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

        public MenuException ( MenuExceptions errorCode=MenuExceptions.ReturningBack , string menu = null) : base(menu)
        {
            ErrorCode = errorCode;
        }

        
    }
}
