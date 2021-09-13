using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

//[assembly: PreApplicationStartMethod(typeof(MVCTesterCPI2.Register.AuthModule), "Initialize")]

namespace MVCTesterCPI2.Register
{
    public class AuthModule : IHttpModule
    {
        //without registering in another file
        //public static void Initialize()
        //{
        //    DynamicModuleUtility.RegisterModule(typeof(AuthModule));
        //}

        public void Init(HttpApplication httpApplication)
        {
            //httpApplication.EndRequest += (src, args) =>
            //{
            //    HttpContext ctx = HttpContext.Current;
            //    ctx.Response.Write(string.Format("<div class='alert alert-success'>URL: {0} Status: {1}</div>",
            //        ctx.Request.RawUrl, ctx.Response.StatusCode));
            //};
            EventHandlerTaskAsyncHelper facilitateAsync = new EventHandlerTaskAsyncHelper(InitiateAuthorization);
            httpApplication.AddOnPostAuthenticateRequestAsync(facilitateAsync.BeginEventHandler, facilitateAsync.EndEventHandler);
        }

        public async Task InitiateAuthorization(object sender, EventArgs eventArgs)
        {

        }

        public void Dispose()
        {

        }
    }
}