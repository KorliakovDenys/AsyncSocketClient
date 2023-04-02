using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NP_PW_Client_1 {
    public static class AsyncClient {
        private static readonly byte[] Buffer = new byte[1024];

        public static async Task ConnectAsync(string serverIp, int port) {
            var ipAddress = IPAddress.Parse(serverIp);

            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                await client.ConnectAsync(ipAddress, port);
                Console.WriteLine($"Connected to {client.RemoteEndPoint}");

                while (true) {
                    Console.WriteLine("Enter a message: ");
                    var message = Console.ReadLine();

                    if (string.IsNullOrEmpty(message)) break;

                    var data = Encoding.Unicode.GetBytes(message);
                    await client.SendAsync(new ArraySegment<byte>(data, 0, data.Length), SocketFlags.None);

                    var bytesReceived = await client.ReceiveAsync(new ArraySegment<byte>(Buffer), SocketFlags.None);
                    var response = Encoding.Unicode.GetString(Buffer, 0, bytesReceived);
                    Console.WriteLine($"Server response: {response}");
                }
            }
            catch (Exception exception) {
                Console.WriteLine($"Error occurred: {exception.Message}");
            }
            finally {
                if (client.Connected) {
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
        }
    }
}