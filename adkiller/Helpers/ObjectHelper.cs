using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller.Helpers
{
    public static class ObjectHelper
    {
        public static void CheckNull(this object value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(String.Format("{0} cannot be null!", name), name);
        }
    }
}
