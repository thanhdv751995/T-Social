using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Hub
{
    public class HubSignalR : Microsoft.AspNetCore.SignalR.Hub
    {
        public void InstallProxyService()
        {
            Clients.All.SendAsync("InstallProxyService");
        }
        public void PingHost(string IP, string status)
        {
            Clients.All.SendAsync("PingHost", IP, status);
        }
        public void CreateScript(bool isDefault)
        {
            Clients.All.SendAsync("CreateScript", isDefault);
        }
        public void UpdateClientUsingScript(string scriptId, string userName)
        {
            Clients.All.SendAsync("UpdateClientUsingScript", scriptId, userName);
        }
        public void SendSecretKeyFromExtensions(string secretKey, string username)
        {
            Clients.All.SendAsync("SendSecretKeyFromExtensions", secretKey, username);
        }
        public void SendF2AFromClient(string f2a, string username)
        {
            Clients.All.SendAsync("SendF2AFromClient", f2a, username);
        }
        public void PostClientActivity(string userName, string content, string Url, string scriptName)
        {
            Clients.All.SendAsync("PostClientActivity", userName, content, Url, scriptName);
        }
        public void PostNotification(string idUser, string content, string urlAvatar, string time)
        {
            Clients.All.SendAsync("PostNotification", idUser, content, urlAvatar, time);
        }
        public void PostGroup(string userName, string groupName, string avatarGroup, string groupUrl, string content)
        {
            Clients.All.SendAsync("PostGroup", userName, groupName, avatarGroup, groupUrl, content);
        }
        public void PostConnection(string connectionId, string clientId)
        {
            Clients.All.SendAsync("PostConnection", connectionId, clientId);
        }
        public void RemoveConnection(string connectionId, string clientId)
        {
            Clients.All.SendAsync("RemoveConnection", connectionId, clientId);
        }

        public override Task OnConnectedAsync()
        {
            var ConnectionID = Context.ConnectionId;
            Clients.Client(ConnectionID).SendAsync("GetIdConnection", ConnectionID);

            Console.WriteLine($"{Context.ConnectionId} has connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"{Context.ConnectionId} has disconnected");
            var ConnectionID = Context.ConnectionId;

            Clients.Client(ConnectionID).SendAsync("RemoveConnectionId", ConnectionID);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
