using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Helpers;

namespace AdKiller
{
    public class AdKillingThread
    {
        public event SoundDataReadyEventHandler ThreadReady;

        public void Process(SoundDataReadyEventArgs dataSet)
        {
            dataSet.CheckNull("thread dataSet");
            OnThreadReady(dataSet);
        }
        private void OnThreadReady(SoundDataReadyEventArgs dataSet)
        {
            if (ThreadReady != null)
            {
                ThreadReady(this, dataSet);
            }
        }
    }
}
