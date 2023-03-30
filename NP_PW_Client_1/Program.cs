using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 25565;

var ip = "192.168.100.100";

try {
    while (true) {
        var ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(ipPoint);
        Console.Write("Введіть повідомлення: ");
        var message = Console.ReadLine();
        var data = Encoding.Unicode.GetBytes(message);
        socket.Send(data);
        Console.WriteLine($"О {DateTime.Now.ToShortTimeString()} до {socket.RemoteEndPoint} відправлено рядок: {message}");
    
        data = new byte[256];
        var builder = new StringBuilder();

        do {
            var bytes = socket.Receive(data, data.Length, 0);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
        } while (socket.Available > 0);

        Console.WriteLine($"О {DateTime.Now.ToShortTimeString()} від {socket.RemoteEndPoint} отримано рядок: {builder}");


        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
catch (Exception e) {
    Console.WriteLine(e.Message);
}
Console.ReadKey();