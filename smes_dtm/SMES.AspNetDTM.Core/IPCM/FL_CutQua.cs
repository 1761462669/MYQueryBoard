using SMES.AspNetDTM.ICore.IPCM;
using SMES.Framework;
using SMES.Framework.EplDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Core.IPCM
{
    public class FL_CutQua : ServiceBase, IFL_CutQua
    {
        public DataTable QueryQua(string strCon)
        {
            string nowdate = DateTime.Now.ToString("yyyy-MM-dd");
            string YMD = nowdate.Substring(0, 7)+"-01";
            string sql = "";
            if (strCon == "1")//制丝质量
            {
                sql = "";
            }
            else if (strCon == "2")//卷包质量
            {

            }

            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

    }
}
