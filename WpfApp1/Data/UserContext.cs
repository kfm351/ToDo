using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.Data
{
    public static class UserContext
    {
        public static User? Current { get; set; }
    }
}
