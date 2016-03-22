using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller.Interfaces
{
    public interface ISoundSource
    {
        event SoundDataReadyEventHandler DataReady;
        event EventHandler Stop;
    }
}
