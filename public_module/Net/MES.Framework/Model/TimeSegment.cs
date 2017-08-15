using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Model
{
    public class TimeSegment : ITimeSegment
    {
        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public TimeSpan Span
        {
            get { return this.EndTime - this.StartTime; }
        }
    }
}
