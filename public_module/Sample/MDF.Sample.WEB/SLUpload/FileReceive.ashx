<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Configuration;
using System.Collections.Specialized;

public class Handler : IHttpHandler,IRequiresSessionState
{

    private HttpContext _httpContext;
    private string _fileName;
    private bool _lastChunk;
    private bool _firstChunk;
    public void ProcessRequest(HttpContext context)
    {
        _httpContext = context;
        
        if (context.Request.InputStream.Length == 0)
            return;
        
            GetQueryStringParameters();

            string uploadFolder = GetUploadFolder();
            string tempFileName = _fileName;
            context.Session["videourl"] = _fileName;
            if (_firstChunk)
            {
                if (File.Exists(@HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + tempFileName))
                    File.Delete(@HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + tempFileName);
                if (File.Exists(@HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + _fileName))
                    File.Delete(@HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + _fileName);

            }
            using (FileStream fs = File.Open(@HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + tempFileName, FileMode.Append))
            {
                SaveFile(context.Request.InputStream, fs);
                fs.Close();
            }
            if (_lastChunk)
            {
                //File.Move(HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + tempFileName, HostingEnvironment.ApplicationPhysicalPath + "/" + uploadFolder + "/" + _fileName);

            }

    }
    private void SaveFile(Stream stream, FileStream fs)
    {
        byte[] buffer = new byte[4096];
        int bytesRead;
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        {
            fs.Write(buffer, 0, bytesRead);
        }
    }
    private void GetQueryStringParameters()
    {
        _fileName = _httpContext.Request.QueryString["file"];
        _firstChunk = string.IsNullOrEmpty(_httpContext.Request.QueryString["first"]) ? true : bool.Parse(_httpContext.Request.QueryString["first"]);
        _lastChunk = string.IsNullOrEmpty(_httpContext.Request.QueryString["last"]) ? true : bool.Parse(_httpContext.Request.QueryString["last"]);
    }
    protected virtual string GetUploadFolder()
    {
        NameValueCollection slupload = ConfigurationManager.GetSection("SLUpload") as NameValueCollection;
        if (!string.IsNullOrEmpty(slupload["File_PublicInfo_Folder"].ToString()))
        {
            return slupload["File_PublicInfo_Folder"].ToString();
        }
        return null;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}