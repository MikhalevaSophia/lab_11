using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab_11_1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var tcpListener = new Server();
            tcpListener.Run();
            Thread.Sleep(1000);

            using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Thread.Sleep(1000);
                tcpClient.Connect("127.0.0.1", 8888);
                if(tcpClient.Connected)
                {
                    Console.WriteLine($"Client: Established connection with {tcpClient.RemoteEndPoint}");
                    Console.WriteLine("Client: Enter Ticker");
                    string message = Console.ReadLine();

                    byte[] requestData = Encoding.UTF8.GetBytes(message);
                    tcpClient.Send(requestData);

                    Console.WriteLine("Client: Request sent");

                        
                    byte[] data = new byte[512];
                
                    int bytes = tcpClient.Receive(data);
                    string response = Encoding.UTF8.GetString(data, 0, bytes);
                    Console.WriteLine($"Client: Received data: {response}");

                }
                else Console.WriteLine("Client: Connection failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Thread.Sleep(1000);

        }
    }
}