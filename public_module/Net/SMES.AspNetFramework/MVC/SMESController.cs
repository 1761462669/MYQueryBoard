using SMES.AspNetFramework.Cfg;
using SMES.AspNetFramework.Excpt;
using SMES.AspNetFramework.Json;
using SMES.AspNetFramework.Log;
using SMES.AspNetFramework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity;
using SMES.AspNetFramework.Model;

namespace SMES.AspNetFramework.MVC
{
    /// <summary>
    /// SMES MVC控制器
    /// </summary>
    public class SMESController : Controller
    {
        #region 属性、变量
        public string LoginUser
        { get; set; }

        public User SMESUser
        { get; set; }
        #endregion

        #region 初始化
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            LoginUser = requestContext.HttpContext.Request["username"];
            SMESUser = new User();

            SMESUser.Id = requestContext.HttpContext.Request["userid"];
            SMESUser.UserName = requestContext.HttpContext.Request["username"];
            SMESUser.DeptId = requestContext.HttpContext.Request["deptid"];

            ViewBag.AppName = Config.GetConfig<AppCfg>().AppName;
            base.Initialize(requestContext); 
        }
        #endregion

        /// <summary>
        /// 系统授权接口
        /// </summary>
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}       

        //private void Authorization()
        //{
        //    string username=Request.QueryString["username"];
        //    if (string.IsNullOrEmpty(username))
        //        return;

        //    var _identity = CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = vm.IsRember, }, _identity);
        // }


        #region 发生异常、错误信息
        protected override void OnException(ExceptionContext filterContext)
        {
            

            RpsData rqd = new RpsData();
            rqd.IsSucceed = false;
            rqd.ErrorCode = 200;

            if (filterContext.Exception.InnerException != null && filterContext.Exception is CustomException)
            {
                CustomException ex = filterContext.Exception.InnerException as CustomException;
                rqd = RpsData.Fail(ex.Code, ex.Message);
            }
            else
            {
                rqd = RpsData.Fail(filterContext.Exception.Message);
                //Log.Write(filterContext.Exception);
                LogHelper.Write(filterContext.Exception);
            }

            rqd.Error = filterContext.Exception.Message;
            filterContext.HttpContext.Response.Clear();

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 200;            

            filterContext.HttpContext.Response.Write(JsonExtend.ToJson(rqd));
            filterContext.HttpContext.Response.Flush();
            filterContext.HttpContext.Response.End();

           
        }
        #endregion

        #region json处理
        public SMESJsonResult JsonSMES(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            SMESJsonResult result = new SMESJsonResult();
            result.Data = data;
            result.ContentType = contentType;
            result.ContentEncoding = contentEncoding;
            result.JsonRequestBehavior = behavior;
            return result;
        }

        protected SMESJsonResult JsonSMES(object data, string contentType, JsonRequestBehavior behavior)
        {
            return this.JsonSMES(data, contentType, null, behavior);
        }

        protected virtual SMESJsonResult JsonSMES(object data, string contentType, Encoding contentEncoding)
        {
            return this.JsonSMES(data, contentType, contentEncoding, JsonRequestBehavior.DenyGet);
        }

        protected internal SMESJsonResult JsonSMES(object data, JsonRequestBehavior behavior)
        {
            return this.JsonSMES(data, null, null, behavior);
        }

        protected internal SMESJsonResult JsonSMES(object data, string contentType)
        {
            return this.JsonSMES(data, contentType, null, JsonRequestBehavior.DenyGet);
        }

        protected internal SMESJsonResult JsonSMES(object data)
        {
            return this.JsonSMES(data, null, null, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}
