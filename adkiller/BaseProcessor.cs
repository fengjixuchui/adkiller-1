using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdKiller.Interfaces;

namespace AdKiller
{
    public abstract class BaseProcessor : ISoundSource
    {
        private ISoundSource source;
        protected abstract void HandleDataReady(object source, SoundDataReadyEventArgs args);
        protected virtual void HandleStop(object source, EventArgs args)
        {
            source = null;
            OnStop();
        }
        protected virtual void OnDataReady(SoundDataReadyEventArgs dataSet)
        {
            if (DataReady != null)
            {
                DataReady(this, dataSet);
            }
        }
        protected virtual void OnStop()
        {
            if (Stop!=null)
            {
                Stop(this, EventArgs.Empty);
            }
        }

        public ISoundSource SoundSource
        {
            get
            {
                return source;
            }
            set
            {
                if (source != null)
                {
                    source.DataReady -= HandleDataReady;
                    source.Stop -= HandleStop;
                }
                source = value;
                if (source != null)
                {
                    source.DataReady += HandleDataReady;
                    source.Stop += HandleStop;
                }
            }
        }
        public event SoundDataReadyEventHandler DataReady;
        public event EventHandler Stop;
    }
}
