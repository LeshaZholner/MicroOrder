using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Concurrent;

namespace MicroOrder.WebApp.Infrastructure.Authentication;

public class MemoryTicketStore : ITicketStore
{
    private readonly ConcurrentDictionary<string, AuthenticationTicket> _tickets;

    public MemoryTicketStore()
    {
        _tickets = new ConcurrentDictionary<string, AuthenticationTicket>();
    }

    public Task RemoveAsync(string key)
    {
        if (_tickets.ContainsKey(key))
        {
            _tickets.TryRemove(key, out var _);
        }

        return Task.CompletedTask;
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        _tickets[key] = ticket;

        return Task.CompletedTask;
    }

    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        _tickets.TryGetValue(key, out var ticket);

        return Task.FromResult(ticket);
    }

    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString();
        var result = _tickets.TryAdd(key, ticket);

        if (!result)
        {
            throw new InvalidOperationException("Failed to store the ticket.");
        }

        return Task.FromResult(key);
    }
}
