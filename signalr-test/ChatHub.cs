using Microsoft.AspNetCore.SignalR;

namespace signalr_test;

public class ChatHub : Hub
{
    public static int TotalPressed { get; set; } = 0;
    public static int TotalUsers { get; set; } = 0;
    public static bool Updated { get; set; } = false;

    public override async Task OnConnectedAsync()
    {
        TotalUsers++;
        Console.WriteLine($"User: {Context.ConnectionId} connected");
        Console.WriteLine($"Total users: {TotalUsers}");
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

    public async Task UpdateMenu()
    {
        Updated = true;
        Console.WriteLine($"Updated: {Updated} -> Notifying the clients...");
        await Clients.All.SendAsync("UpdateMenuReceived", Updated);
        Updated = false;
        Console.WriteLine($"Updated: {Updated}");
    }

    public async Task SendMessage(string from, string to, string message)
    {
        await Clients.Client(to).SendAsync("UpdateMenuReceived", from, message);
    }

    // Notify the caller of the method
    //public async Task NotifyJustOne(string user, string message)
    //{
    //  await Clients.Caller.SendAsync("notifyJustOneReceived", user, message)
    //}

    // Notify everyone other than the caller: Clients.Others.SendAsync("notifyOthersReceived", user, message)

    // Notify specific id: await Clients.Client("connection id A").SendAsync("notifySpecificUser", user, message);

    // Notify multiple ids: await Clients.Clients("id 1", "id 2").SendAsync("notifyMany", user, message);

    // Notify all (included the sender) except some: await Clients.AllExcept("id 1").SensAsync("notifyExceptReceived", user, message);

    // Specific user by some your ID: await Clients.User("yourname@gmail.com").SendAsync("notifySpecificUser", user, mes

}
