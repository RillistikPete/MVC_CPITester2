using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(MVCTesterCPI2.Register.RegMod), "RegisterAtStart")]

namespace MVCTesterCPI2.Register
{
    public static class RegMod
    {
        public static void RegisterAtStart()
        {
            HttpApplication.RegisterModule(typeof(AuthModule));
        }
    }
}