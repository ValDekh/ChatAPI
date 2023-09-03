using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.EventHandlers
{
    public class BaseEventHandler<TArgs> where TArgs : EventArgs
    {
        public event EventHandler<TArgs> OnCreate;
        public void CreateInvoke(TArgs args)
        {
            OnCreate?.Invoke(this, args);
        }
    }
}
