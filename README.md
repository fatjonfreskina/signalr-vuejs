# signalr-vuejs

## Setup

- Frontend: npm install && npm run serve

- Backend aprire la solution e avviare la web api

## Documentazione

Preso spunto da questp [video](https://www.youtube.com/watch?v=pl0OobPmWTk&t=2275s&ab_channel=DotNetMastery)

## Test

Si possono provare:
- GET test , è un modo per agganciare le (check swagger per testare) chiamate http a signalR, serve dependency injection. Check il controller
- Aumentare ed osservare variabili tra tutti gli utenti connessi (contatore utenti connessi e contatore bottone)
- Messaggi diretti tramite i gruppi: provare aprendo più tab, joinando una stanza con lo stesso room id, e mandare un messaggio. Il messaggio verrà ricevuto da tutti gli utenti della stanza

## Notes

### C\# options and notes

```csharp
// Notify the caller of the method
public async Task NotifyJustOne(string user, string message)
{
  await Clients.Caller.SendAsync("notifyJustOneReceived", user, message)
}

// Notify everyone other than the caller: 
public async Task NotifyOthers(string user, string message)
{
    Clients.Others.SendAsync("notifyOthersReceived", user, message)
}

public async Task NotifyOneByConnectionId(string user, string message)
{
    await Clients.Client("connection id A").SendAsync("notifySpecificUser", user, message);
}

public async Task NotifyAllExcept(string exceptId)
{
    await Clients.AllExcept(exceptId).SendAsync("notifyAllExcept", user, message);
}

public async Task NotifySpecificUserById(string userId, string message)
{
  await Clients.User("yourname@gmail.com").SendAsync("notifySpecificUser", message);
}
```

### Javascript options and notes

- Difference between `send` and `invoke`: Send does not expect a response from the server. Invoke does await for a value returned.
