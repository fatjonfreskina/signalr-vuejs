# signalr-vuejs

## Setup

Frontend: npm install && npm run serve

Backend aprire la solution e avviare la web api

## Documentazione

Continuare: [video](https://www.youtube.com/watch?v=pl0OobPmWTk&t=2275s&ab_channel=DotNetMastery)

## Test

- Per ora funzionano: il contatore utenti attivi, la GET, e il contatore del bottone.

Manca da capire i messaggi diretti, in particolare come mappare gli id...

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

// Notify multiple ids: await Clients.Clients("id 1", "id 2").SendAsync("notifyMany", user, message);

// Notify all (included the sender) except some: await Clients.AllExcept("id 1").SensAsync("notifyExceptReceived", user, message);

// Specific user by some your ID: await Clients.User("yourname@gmail.com").SendAsync("notifySpecificUser", user, message);
```

### Javascript options and notes

- Difference between `send` and `invoke`: Send does not expect a response from the server. Invoke does await for a value returned.
