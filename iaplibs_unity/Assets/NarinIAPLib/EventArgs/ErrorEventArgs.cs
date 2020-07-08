using System;
using System.Collections.Generic;

namespace Narin.Unity.IAP {

    public class ErrorEventArgs:EventArgs {
        public readonly string Message;
        
        public ErrorEventArgs(string message) {
            Message = message;
        }
    }

}