namespace NP_PW_Client_1;

class Program {
    public static async Task Main(string[] args) {
        await AsyncClient.ConnectAsync("192.168.100.101", 8888);
    }
}

