using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Model
{
    public class Plan : TimeSegment, IPlan
    {
        public string Id { get; set; }


        public string ResourceId { get; set; }


        public string ResourceName { get; set; }


        public string Code { get; set; }


        public string ProductId { get; set; }


        public decimal Qua { get; set; }


        public string UnitId { get; set; }


        public string UnitName { get; set; }


        public int Seq { get; set; }



        public string ProductName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string StateId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string StateName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
