using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            int port;
            string ipSTR;
            using (var db = new AppContext())
            {
                var connectionInfo = db.ConnectionInfos.OrderBy(x=>x.Id).Last();
                
                    ipSTR= connectionInfo.IpAddress;
                    port = Convert.ToInt32(connectionInfo.Port);
                
            }

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var ip = IPAddress.Parse(ipSTR);
            var listenerEP = new IPEndPoint(ip,port);
            listener.Bind(listenerEP);

            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var buffer = new byte[ushort.MaxValue];
            var segment = new ArraySegment<byte>(buffer);
            while (true)
            {
                var result = await listener.ReceiveFromAsync(segment, SocketFlags.None, ep);
                int len = result.ReceivedBytes;
                var endPoint = result.RemoteEndPoint;
                var str = Encoding.Default.GetString(buffer, 0, len);
                Console.WriteLine($"{endPoint} : {str}");
            }

        }

    }
}