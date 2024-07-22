using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemotingServer;

namespace RemotingClient
{
    class Program
    {
        static void Main()
        {
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);

            RemoteObject obj = (RemoteObject)Activator.GetObject(
                typeof(RemoteObject),
                "tcp://localhost:1234/RemoteObject");

            if (obj == null)
            {
                Console.WriteLine("Could not locate server");
            }
            else
            {
                string result = obj.SayHello("World");
                Console.WriteLine(result);
            }
        }
    }
}
