using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller
{
    public class ModuleIniti : IDisposable
    {
        public SoundIn Source { get; private set; }
        public AdDetector AdRemover { get; private set; }
        public SoundOut Out { get; private set; }

        private FMOD.System system;
        private AdKillerMainForm adKillerForm;

        public ModuleIniti(AdKillerMainForm adForm)
        {
            var result = FMOD.Factory.System_Create(ref system);
            Sound.ErrorCheck(result);
            result = system.init(4, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
            Sound.ErrorCheck(result);
            this.adKillerForm = adForm;
            // result = system.setStreamBufferSize(64 * 1024, FMOD.TIMEUNIT.RAWBYTES);
            // Sound.ErrorCheck(result);
        }
        public void Process(string startJinglePath, string endJinglePath, string connectionString)
        {
            ModuleInitJingle(startJinglePath, endJinglePath);
            ModuleInitSound(connectionString);
            adKillerForm.Invoke(new UpdateStatusLabelCallback(adKillerForm.UpdateStatusLabel), new object[] { "lala" });
        }
        private void ModuleInitJingle(string startJinglePath, string endJinglePath)
        {
            AdRemover = new AdDetector(Sound.CreateJingle(system, startJinglePath), Sound.CreateJingle(system, endJinglePath));
        }
        private void ModuleInitSound(string connectionString)
        {
            
            Source = new SoundIn(this.system, connectionString);
            Out = new SoundOut(this.system);
            AdRemover.SoundSource = Source;
            Out.SoundSource = AdRemover;
        }
        public void Dispose()
        {
            if (Source != null)
            {
                Source.StopMe();
                Source = null;
            }
            if (Out != null)
            {
                Out = null;
            }
        }

        public delegate void UpdateStatusLabelCallback(string text);
    }
}
