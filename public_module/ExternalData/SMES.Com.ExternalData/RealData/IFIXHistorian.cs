using Proficy.Historian.ClientAccess.API;
using SMES.Com.IExternalData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Com.ExternalData.RealData
{
    public class IFIXHistorian : IHistorian, IDisposable
    {

        /// <summary>
        /// IFIX 服务器名 可对应配置 IFIX_HostName
        /// </summary>
        public string HostName
        { get; set; }

        /// <summary>
        /// 用户名 对应配置 IFIX_UserName
        /// </summary>
        public string UserName
        { get; set; }

        /// <summary>
        /// 密码 对应配置 IFIX_Password
        /// </summary>
        public string Password
        { get; set; }

        private ServerConnection _connection;
        private ServerConnection Connection
        {
            get
            {
                if (_connection == null)
                    CreateConnection();

                return _connection;
            }
        }

        public IFIXHistorian()
        {
            HostName = System.Configuration.ConfigurationManager.AppSettings["IFIX_HostName"];
            UserName = System.Configuration.ConfigurationManager.AppSettings["IFIX_UserName"];
            Password = System.Configuration.ConfigurationManager.AppSettings["IFIX_Password"];
        }

        /// <summary>
        /// 创建IFIXHistorian连接
        /// </summary>
        /// <param name="hostname">服务器名称</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public IFIXHistorian(string hostname, string username, string password)
        {
            HostName = hostname;
            UserName = username;
            Password = password;
        }

        private ServerConnection CreateConnection()
        {
            _connection = new ServerConnection(new ConnectionProperties { ServerHostName = HostName, Username = UserName, Password = Password, ServerCertificateValidationMode = CertificateValidationMode.None });
            _connection.Connect();
            return _connection;
        }

        public object GetValue(string tag)
        {
            DataQueryParams query = new CurrentValueQuery(tag);
            ItemErrors errors;
            Proficy.Historian.ClientAccess.API.DataSet dataset = null;

            Connection.IData.Query(ref query, out dataset, out errors);

            if (dataset.TotalSamples > 0)
                return dataset[tag].GetValue(0);
            else
                return "";   
        }

        public Dictionary<string, object> GetValues(params string[] tags)
        {
            if (tags == null || tags.Length == 0)
                return new Dictionary<string, object>();

            DataQueryParams query = new CurrentValueQuery(tags);
            ItemErrors errors;
            Proficy.Historian.ClientAccess.API.DataSet dataset = null;
            Connection.IData.Query(ref query, out dataset, out errors);

            Dictionary<string, object> values = new Dictionary<string, object>();

            foreach (var item in dataset)
            {
                if (item.Value.Count() > 0)
                    values.Add(item.Key, item.Value.GetValue(0));
            }

            foreach (string key in tags)
            {
                if (dataset.ContainsKey(key))
                    values[key] = dataset[key].GetValue(0);
                else
                    values[key] = "";
            }

            return values;
        }

        public System.Data.DataTable ReplaceTableCellValue(System.Data.DataTable dt, params string[] cols)
        {
            if (dt == null || cols == null || cols.Length == 0)
                return dt;

            DataTable tmptable = dt.Copy();

            List<string> tags = null;

            foreach (string tagcol in cols)
            {
                tags = new List<string>();

                if (tmptable.Columns[tagcol] == null)
                    continue;

                for (int i = 0; i < tmptable.Rows.Count; i++)
                {
                    if (tmptable.Rows[i][tagcol].ToString() == "")
                        continue;

                    tags.Add(tmptable.Rows[i][tagcol].ToString());
                }

                if (tags.Count == 0)
                    continue;

                Dictionary<string, object> values = GetValues(tags.ToArray());

                string tmptagname = "";
                for (int i = 0; i < tmptable.Rows.Count; i++)
                {
                    tmptagname = tmptable.Rows[i][tagcol].ToString();

                    if (values[tmptagname] != null)
                    {
                        tmptable.Rows[i][tagcol] = values[tmptagname];
                    }
                }

            }

            return tmptable;
        }

        public System.Data.DataTable AddTableColumnValue(System.Data.DataTable dt, params string[] cols)
        {
            if (dt == null || cols == null || cols.Length == 0)
                return dt;

            DataTable tmptable = dt.Copy();

            List<string> tags = null;

            foreach (string tagcol in cols)
            {
                tags = new List<string>();

                if (tmptable.Columns[tagcol] == null)
                    continue;

                for (int i = 0; i < tmptable.Rows.Count; i++)
                {
                    if (tmptable.Rows[i][tagcol].ToString() == "")
                        continue;

                    tags.Add(tmptable.Rows[i][tagcol].ToString());
                }

                if (tags.Count == 0)
                    continue;

                Dictionary<string, object> values = GetValues(tags.ToArray());

                string tmptagname = "";
                tmptable.Columns.Add(tagcol + "VALUE");
                for (int i = 0; i < tmptable.Rows.Count; i++)
                {
                    tmptagname = tmptable.Rows[i][tagcol].ToString();

                    if (values[tmptagname] != null)
                    {
                        tmptable.Rows[i][tagcol + "VALUE"] = values[tmptagname];
                    }
                }

            }

            return tmptable;
        }

        public void Dispose()
        {
            if (_connection != null && Connection.IsConnected())
            {
                try
                {
                    _connection.Disconnect();
                    _connection.Dispose();
                }
                catch { }
            }
        }
    }
}
