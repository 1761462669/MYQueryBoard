using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Model.Pub
{
    [Serializable]
    public class SqlCommandConfigModel
    {
        public int Id
        { get; set; }
        public string Code { get; set; }

        public string Script { get; set; }

        public bool IsUsed { get; set; }

        public string PageNm { get; set; }

        public string StrCon { get; set; }

        public string Remark { get; set; }

    }
}
