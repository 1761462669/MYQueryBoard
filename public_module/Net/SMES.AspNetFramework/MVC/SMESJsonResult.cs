using SMES.AspNetFramework.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SMES.AspNetFramework.Json;

namespace SMES.AspNetFramework.MVC
{
    /// <summary>
    /// SMES JsonResult
    /// </summary>
    public class SMESJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentException("context");

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
                response.ContentType = ContentType;
            else
                response.ContentType = "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                //JavaScriptSerializer serializer = new JavaScriptSerializer();

                //response.Write(serializer.Serialize(Data));

                RpsData rqsdata = new RpsData();

                string jsonstr = "";
                if (Data.GetType().Name == "DataTable")
                {
                    rqsdata = RpsData.Succee((Data as DataTable).ToJArray());
                    jsonstr = JsonExtend.ToJson(rqsdata);
                }
                //jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject((Data as DataTable).ToJArray());
                else if (Data.GetType().Name == "DataRow")
                {
                    rqsdata = RpsData.Succee((Data as DataRow).ToJObject());
                    jsonstr = JsonExtend.ToJson(rqsdata);
                }
                //jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject((Data as DataRow).ToJObject());
                else
                {
                    rqsdata = RpsData.Succee(Data);
                    jsonstr = JsonExtend.ToKonwnTypeJson(rqsdata);
                }
                //jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(Data);

                response.Write(jsonstr);

            }
            else
            {
                response.Write(JsonExtend.ToKonwnTypeJson(RpsData.Succee(null)));
            }
        }
    }
}
