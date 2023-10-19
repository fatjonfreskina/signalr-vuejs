using Microsoft.AspNetCore.SignalR;

namespace signalr_test;

public class ChatHub : Hub
{
    public static int TotalPressed { get; set; } = 0;
    public static int TotalUsers { get; set; } = 0;

    public override async Task OnConnectedAsync()
    {
        TotalUsers++;
        Console.WriteLine($"Users: {TotalUsers}");
        await Clients.All.SendAsync("updateTotalUsers", TotalUsers, TotalPressed);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        TotalUsers--;
        Console.WriteLine($"Users: {TotalUsers}");
        await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
    }
    public async Task AddOne()
    {
        TotalPressed++;
        Console.WriteLine($"Pressed: {TotalPressed}");
        await Clients.All.SendAsync("addOneReceived", TotalPressed);
    }
}
