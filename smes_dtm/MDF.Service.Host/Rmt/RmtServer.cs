using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Service.Host.Rmt
{
    public class RmtServer
    {
        private TcpServerChannel server;

        public int Port
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["RmtProt"]);
            }
        }

        public string SericeName
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceName"];
            }
        }

        public void Start()
        {
            //int port = int.Parse(ConfigurationManager.AppSettings["RmtProt"]);
            //string serverName = ConfigurationManager.AppSettings["ServiceName"];
            server = new TcpServerChannel(SericeName, Port);
            ChannelServices.RegisterChannel(server, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Service), SericeName, WellKnownObjectMode.SingleCall);
            RemotingConfiguration.CustomErrorsEnabled(false);
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            
        }

        public void Stop()
        {
            ChannelServices.UnregisterChannel(server);
        }
    }
}
