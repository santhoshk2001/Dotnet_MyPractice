using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    public class RemoteObject : MarshalByRefObject
    {
        public string SayHello(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
