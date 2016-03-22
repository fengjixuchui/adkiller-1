using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller.Helpers
{
   public static class FMODHelper
    {
      
            [DllImport(FMOD.VERSION.dll, CharSet = CharSet.Unicode)]
            private static extern FMOD.RESULT FMOD_System_CreateSound(IntPtr systemraw, IntPtr data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref IntPtr sound);

            [DllImport(FMOD.VERSION.dll, CharSet = CharSet.Unicode)]
            private static extern FMOD.RESULT FMOD_System_CreateSound(IntPtr systemraw, short[] data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref IntPtr sound);

            [DllImport(FMOD.VERSION.dll, CharSet = CharSet.Unicode)]
            private static extern FMOD.RESULT FMOD_System_CreateStream(IntPtr systemraw, IntPtr data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref IntPtr stream);

            [DllImport(FMOD.VERSION.dll, CharSet = CharSet.Unicode)]
            private static extern FMOD.RESULT FMOD_System_CreateStream(IntPtr systemraw, short[] data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref IntPtr stream);

            public static FMOD.RESULT createSound(this FMOD.System system, short[] data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref FMOD.Sound sound)
            {
                FMOD.RESULT result = FMOD.RESULT.OK;
                IntPtr soundraw = new IntPtr();
                FMOD.Sound soundnew = null;

                try
                {
                    result = FMOD_System_CreateSound(system.getRaw(), data, mode, ref exinfo, ref soundraw);
                }
                catch
                {
                    result = FMOD.RESULT.ERR_INVALID_PARAM;
                }
                if (result != FMOD.RESULT.OK)
                {
                    return result;
                }

                if (sound == null)
                {
                    soundnew = new FMOD.Sound();
                    soundnew.setRaw(soundraw);
                    sound = soundnew;
                }
                else
                {
                    sound.setRaw(soundraw);
                }

                return result;
            }

            public static FMOD.RESULT createStream(this FMOD.System system, short[] data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref FMOD.Sound sound)
            {
                FMOD.RESULT result = FMOD.RESULT.OK;
                IntPtr soundraw = new IntPtr();
                FMOD.Sound soundnew = null;

                mode = mode | FMOD.MODE.UNICODE;

                try
                {
                    result = FMOD_System_CreateStream(system.getRaw(), data, mode, ref exinfo, ref soundraw);
                }
                catch
                {
                    result = FMOD.RESULT.ERR_INVALID_PARAM;
                }
                if (result != FMOD.RESULT.OK)
                {
                    return result;
                }

                if (sound == null)
                {
                    soundnew = new FMOD.Sound();
                    soundnew.setRaw(soundraw);
                    sound = soundnew;
                }
                else
                {
                    sound.setRaw(soundraw);
                }

                return result;
            }

            public static FMOD.RESULT createSound(this FMOD.System system, IntPtr data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref FMOD.Sound sound)
            {
                FMOD.RESULT result = FMOD.RESULT.OK;
                IntPtr soundraw = new IntPtr();
                FMOD.Sound soundnew = null;

                try
                {
                    result = FMOD_System_CreateSound(system.getRaw(), data, mode, ref exinfo, ref soundraw);
                }
                catch
                {
                    result = FMOD.RESULT.ERR_INVALID_PARAM;
                }
                if (result != FMOD.RESULT.OK)
                {
                    return result;
                }

                if (sound == null)
                {
                    soundnew = new FMOD.Sound();
                    soundnew.setRaw(soundraw);
                    sound = soundnew;
                }
                else
                {
                    sound.setRaw(soundraw);
                }

                return result;
            }

            public static FMOD.RESULT createStream(this FMOD.System system, IntPtr data, FMOD.MODE mode, ref FMOD.CREATESOUNDEXINFO exinfo, ref FMOD.Sound sound)
            {
                FMOD.RESULT result = FMOD.RESULT.OK;
                IntPtr soundraw = new IntPtr();
                FMOD.Sound soundnew = null;

                mode = mode | FMOD.MODE.UNICODE;

                try
                {
                    result = FMOD_System_CreateStream(system.getRaw(), data, mode, ref exinfo, ref soundraw);
                }
                catch
                {
                    result = FMOD.RESULT.ERR_INVALID_PARAM;
                }
                if (result != FMOD.RESULT.OK)
                {
                    return result;
                }

                if (sound == null)
                {
                    soundnew = new FMOD.Sound();
                    soundnew.setRaw(soundraw);
                    sound = soundnew;
                }
                else
                {
                    sound.setRaw(soundraw);
                }

                return result;
            }
        }
    
}
