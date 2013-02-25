using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonTool
{
    public class DREventArgs : EventArgs
    {
        public string msg = "";
        public DREventArgs(string message)
        {
            msg = message;
        }
    }

    public class ThreadContainer
    {
        //Delegate of event
        public delegate void MsgEventHandler(object sender, DREventArgs e);

        //Event
        public event MsgEventHandler MessageSend;

        //Thread
        public Thread tThread;

        //Start thread
        public void Start()
        {
            tThread = new Thread(StartListener);
            tThread.Start();
            if (!tThread.IsAlive)
                tThread.Abort();
        }

        public void Stop()
        {
            tThread.Abort();
        }

        public virtual void OnSendMessage(object sender, DREventArgs e)
        {
            if (MessageSend != null)
            {
                this.MessageSend(sender, e);
            }
        }

        virtual public void StartListener()
        {
            return;
        }
    }


}
