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
    public class ZSCJ_ZSProductKB: ServiceBase,IZSCJ_ZSProductKB
    {

        public System.Data.DataTable GetYear_DLRate()
        {
            string sql = @"SELECT C.PLANDATE,CASE WHEN C.YXDL  IS NOT NULL THEN C.YXDL  ELSE 0.00 END YXDL  ,CASE WHEN D.GXDL IS NOT NULL THEN D.GXDL ELSE 0.00 END GXDL FROM 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_DL/A.BATCH_SUM*100) YXDL  FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM  DPS.CUTBATCH CB
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_DL
                            FROM RTC.BREAK_RESULT BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),1,7))B ON A.PLANDATE=B.PLANDATE) C
                            LEFT JOIN 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_DL/A.BATCH_SUM*100) GXDL FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM  DPS.CUTBATCH CB
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_DL
                            FROM RTC.BREAK_RESULT BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),1,7))B ON A.PLANDATE=B.PLANDATE) D ON C.PLANDATE=D.PLANDATE
                            ORDER BY PLANDATE ";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable GetMonth_DLRate()
        {
            string sql = @"SELECT C.PLANDATE,CASE WHEN C.YXDL  IS NOT NULL THEN C.YXDL  ELSE 0.00 END YXDL  ,CASE WHEN D.GXDL IS NOT NULL THEN D.GXDL ELSE 0.00 END GXDL FROM 
                            (SELECT A.PLANDATE,
                            CASE WHEN B.BATCH_DL IS NULL  THEN 0 ELSE CONVERT(DECIMAL(10,2), B.BATCH_DL/A.BATCH_SUM*100) END YXDL  ,A.PDATE FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM,CB.PLANDATE PDATE
                            FROM  DPS.CUTBATCH CB
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2),CB.PLANDATE)A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_DL
                            FROM RTC.BREAK_RESULT BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),9,2))B ON A.PLANDATE=B.PLANDATE) C
                            LEFT JOIN 
                            (SELECT A.PLANDATE,
                            CASE WHEN B.BATCH_DL IS NULL  THEN 0 ELSE CONVERT(DECIMAL(10,2), B.BATCH_DL/A.BATCH_SUM*100) END GXDL FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM  DPS.CUTBATCH CB
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_DL
                            FROM RTC.BREAK_RESULT BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,BR.STARTTIME),9,2))B ON A.PLANDATE=B.PLANDATE) D ON C.PLANDATE=D.PLANDATE
                            ORDER BY C.PDATE ";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        public DataTable GetYear_HGRate()
        {
            string sql = @"SELECT C.PLANDATE,CASE WHEN C.YXNY  IS NOT NULL THEN C.YXNY  ELSE 100.00 END YXNY ,CASE WHEN D.GXNY IS NOT NULL THEN D.GXNY ELSE 100.00 END GXNY FROM 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_PASS/A.BATCH_SUM*100) YXNY  FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_PASS 
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND BR.Determine='合格' AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))B ON A.PLANDATE=B.PLANDATE) C
                            LEFT JOIN 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_PASS/A.BATCH_SUM*100) GXNY FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_PASS 
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7)>SUBSTRING( CONVERT(VARCHAR, DATEADD(M,-12,GETDATE()),23),1,7) AND BR.Determine='合格' AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),1,7))B ON A.PLANDATE=B.PLANDATE) D ON C.PLANDATE=D.PLANDATE
                            ORDER BY PLANDATE";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        public DataTable GetMonth_HGRate()
        {
            string sql = @"SELECT C.PLANDATE,CASE WHEN C.YXNY  IS NOT NULL THEN C.YXNY  ELSE 100.00 END YXNY ,CASE WHEN D.GXNY IS NOT NULL THEN D.GXNY ELSE 100.00 END GXNY FROM 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_PASS/A.BATCH_SUM*100) YXNY  ,A.PDATE FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM,CB.PLANDATE PDATE
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2),CB.PLANDATE)A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_PASS 
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND BR.Determine='合格' AND CB.PROCESSROUTINGNAMEFIRSTPSG='叶线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2))B ON A.PLANDATE=B.PLANDATE) C
                            LEFT JOIN 
                            (SELECT A.PLANDATE,CONVERT(DECIMAL(10,2), B.BATCH_PASS/A.BATCH_SUM*100) GXNY FROM 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_SUM
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2))A
                            LEFT JOIN 
                            (SELECT SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2) PLANDATE,CONVERT(DECIMAL, COUNT(*)) BATCH_PASS 
                            FROM RTC.batch_results BR
                            LEFT JOIN DPS.CUTBATCH CB ON BR.Batchno=CB.PLANCODE
                            WHERE SUBSTRING( CONVERT (varchar,CB.PLANDATE),1,10)>SUBSTRING( CONVERT(VARCHAR, DATEADD(D,-20,GETDATE()),23),1,10) AND BR.Determine='合格' AND CB.PROCESSROUTINGNAMEFIRSTPSG='梗线'
                            GROUP BY SUBSTRING(CONVERT(VARCHAR,CB.PLANDATE),9,2))B ON A.PLANDATE=B.PLANDATE) D ON C.PLANDATE=D.PLANDATE
                            ORDER BY C.PDATE";
            DataSet ds = DataAccess.CreateDb("cutstore").ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
