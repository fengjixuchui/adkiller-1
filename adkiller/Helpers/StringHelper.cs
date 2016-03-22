using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdKiller.Helpers
{
   public static class StringHelper
    {
       public static string FillWith (this string s, params object[] args)
       {
           return String.Format(s,args);
       }
    }
}
