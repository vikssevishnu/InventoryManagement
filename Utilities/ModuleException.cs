using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Utilities
{
    public class ModuleException:Exception
    {        
        public long ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }

        public ModuleException(long errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
