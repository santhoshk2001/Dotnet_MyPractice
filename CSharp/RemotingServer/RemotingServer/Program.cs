using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
namespace RemotingServer
{
    class Program
    {
        static void Main()
        {
            TcpChannel channel = new TcpChannel(1234);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RemoteObject),
                "RemoteObject",
                WellKnownObjectMode.Singleton);

            Console.WriteLine("Server started...");
            Console.ReadLine(); // Keeps the server alive
        }
    }
}
