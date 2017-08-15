using MDF.Framework.Db;
using SMES.Framework.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{

    public class ServiceBase : MarshalByRefObject
    {
        [Import]
        public IDataBase Db { get; set; }


        protected virtual PaginationData<T> QueryPage<T>(IQueryable<T> query, int pagesize, int pageindex) where T:class
        {
            if (pageindex < 1)
                pageindex = 1;

            if (pagesize < 1)
                pagesize = 10;

            PaginationData<T> page = new PaginationData<T>();
            page.PageIndex = pageindex;
            page.TotalRecordNum = query.Count();
            page.PageSize = pagesize;

            page.TotalPageNum = page.TotalRecordNum / page.PageSize;

            if (page.TotalRecordNum % page.PageSize != 0)
                page.TotalPageNum++;

            if (page.TotalPageNum == 0)
                page.TotalPageNum = 1;

            if (pageindex > page.TotalPageNum)
                pageindex = page.TotalPageNum;

            page.Data = query.Skip(pagesize * (pageindex-1)).Take(pagesize).ToList();

            return page;
        }
    }
}
