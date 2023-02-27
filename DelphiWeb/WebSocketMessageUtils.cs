using DelphiSupervisorV6;
using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace DelphiWeb
{
    public class WebSocketMessageUtils
    {

        public WebSocketReceiveResult result;

        public WebSocketMessageUtils()
        {

        }

        public async Task SendServiceAddedMessage(ConfiguredService configuredService, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var serverMsg = Encoding.UTF8.GetBytes($"Service added : {configuredService.ServiceName}");
            var result = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
        }

        public void SendServiceDeletedMessage(string configuredService, WebSocket webSocket)
        {
            var serverMsg = Encoding.UTF8.GetBytes($"Service deleted : {configuredService}");

        }

        public void SendServiceStartedMessage(ConfiguredService configuredService, WebSocket webSocket)
        {
            var serverMsg = Encoding.UTF8.GetBytes($"Service started : {configuredService.ServiceName}");

        }

        public void SendServiceStopedMessage(ConfiguredService configuredService, WebSocket webSocket)
        {
            var serverMsg = Encoding.UTF8.GetBytes($"Service stoped : {configuredService.ServiceName}");

        }
    }
}
