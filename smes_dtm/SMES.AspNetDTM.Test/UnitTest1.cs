using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetDTM.Model.Pub;
using System.Collections.Generic;
using System.Data;
using SMES.AspNetDTM.ICore.IPCM;
using SMES.AspNetDTM.Core.IPCM;

namespace SMES.AspNetDTM.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initial()
        {
            //try
            //{
            //    Db = MDF.Framework.Bus.ObjectFactory.GetObject<IDataBase>();

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            IMaterialService service = MDF.Framework.Bus.ObjectFactory.GetObject<IMaterialService>();
            List<Material> list = service.Query();
        }

        [TestMethod]

        public void TestDatasetData()
        {
            
            IMaterialService service = MDF.Framework.Bus.ObjectFactory.GetObject<IMaterialService>();

            DataSet ds = service.QueryDataset();
        }

        [TestMethod]
        public void Test3()
        {
            ITsStockService server = MDF.Framework.Bus.ObjectFactory.GetObject<ITsStockService>();

            ITsStockService server2 = MDF.Framework.Bus.ObjectFactory.GetObject<ITsStockService>("SMES.AspNetDTM.Core.IPCM.TsStockService");

            server.GetSiloInfos();
        }
    }
}
