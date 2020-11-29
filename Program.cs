using System;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;

namespace http_tunnel
{
    class Program
    {
        public void CreateTunnelAndSendRequest(string targetHost, int targetPort, string proxyHost, int proxyPort){
            byte[] buffer = new byte[1024];
            int bytes;

            TcpClient client = new TcpClient(proxyHost, proxyPort);
            NetworkStream stream = client.GetStream();

            byte[] tunnelRequest = Encoding.UTF8.GetBytes($"CONNECT {targetHost}:{targetPort}  HTTP/1.1\r\nHost: {targetHost}\r\n\r\n");
            stream.Write(tunnelRequest, 0, tunnelRequest.Length);
            stream.Flush();

            // If proxy is established TCP connection with the destination successfully,
            //  it should return "HTTP/1.1 200 Connection Established".
            bytes = stream.Read(buffer, 0, buffer.Length);
            Console.Write(Encoding.UTF8.GetString(buffer, 0, bytes));

            SslStream sslStream = new SslStream(stream);
            sslStream.AuthenticateAsClient(targetHost);

            byte[] request = Encoding.UTF8.GetBytes($"GET https://{targetHost}/  HTTP/1.1\r\nHost: {targetHost}\r\n\r\n");
            sslStream.Write(request, 0, request.Length);
            sslStream.Flush();

            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                Console.Write(Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (bytes != 0);

            client.Close();
            Console.ReadLine();
        }

        private void test1(){
            string targetHost = "www.msn.com";
            int targetPort = 443;
            // Use fiddler as forward proxy
            string proxyHost = "127.0.0.1";
            int proxyPort = 8888;

            CreateTunnelAndSendRequest(targetHost, targetPort, proxyHost, proxyPort);
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.test1();
        }
    }
}
