using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller
{
    public class DetectionResult
    {
        public int StartJingleIndex { get; private set; }
        public int EndJingleIndex { get; private set; }

        public DetectionResult(int startIndex, int endIndex)
        {
            StartJingleIndex = startIndex;
            EndJingleIndex = endIndex;
        }
    }
}
