using Lab_11;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab_11_1
{
    class Server
    {
        Socket tcpListener;
        public Server (){
            tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public async void Run(){
            try
            {   
                tcpListener.Bind(new IPEndPoint(IPAddress.Any, 8888));
                tcpListener.Listen();
                Console.WriteLine("Server: Server is up");

                while (true){
                    using (var tcpClient = await tcpListener.AcceptAsync())
                    {
                        Console.WriteLine($"Server: Established connection with {tcpClient.RemoteEndPoint}");
                        
                        byte[] requestData = new byte[512];
                        int bytes = 0;
                        
                        bytes = await tcpClient.ReceiveAsync(requestData, SocketFlags.None);
                        string request = Encoding.UTF8.GetString(requestData, 0, bytes);
                        
                        Console.WriteLine($"Server: Received request: {request}");

                        using (var dataBase = new TickerContext())
                        {   
                            int tickerId = dataBase.TickerEntries.First(item => item.Name == request).Id;
                            string response = dataBase.PriceEntries.Where(item => item.TickerId == tickerId).OrderBy(item => item.Date).Last().Price.ToString();
                            byte[] data = Encoding.UTF8.GetBytes($"The latest price of {request} was ${response}");
                            await tcpClient.SendAsync(data, SocketFlags.None);
                        }


                        Console.WriteLine($"Server: Sent data to {tcpClient.RemoteEndPoint}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Server: Server is down");
                Console.WriteLine(ex.Message);
            }
        }
    }
}