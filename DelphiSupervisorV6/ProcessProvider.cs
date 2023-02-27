using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DelphiSupervisorV6
{   
    public class ProcessProvider
    {
        delegate void WebSocketMessageRecieved(string message);
        event WebSocketMessageRecieved messageReceived;

        private HttpClient client;

        public ProcessProvider()
        {
            client = new HttpClient();
        }

        public List<ProcessInfo> GetAllConfigureServices()
        {
            List<ProcessInfo> processInfo = null;
            HttpResponseMessage httpResponseMessage = client.GetAsync(@"https://localhost:7131/api/processes/processConfigured").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<List<ProcessInfo>>().Result;
            }
            return processInfo;
        }

        public List<ProcessInfo> GetAllProcesses()
        {
            List<ProcessInfo> processInfo = null;
            HttpResponseMessage httpResponseMessage = client.GetAsync(@"https://localhost:7131/api/processes").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<List<ProcessInfo>>().Result;
            }
            return processInfo;
        }

        public ProcessInfo GetByID(int pid)
        {
            ProcessInfo processInfo = null;
            HttpResponseMessage httpResponseMessage = client.GetAsync($"https://localhost:7131/api/processes/processId?id={pid}").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<ProcessInfo>().Result;
            }
            return processInfo;
        }

        public List<ProcessInfo> GetProcessByName(string name)
        {
            List<ProcessInfo> processInfo = null;
            HttpResponseMessage httpResponseMessage = client.GetAsync($"https://localhost:7131/api/processes/processName?Name={name}").Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<List<ProcessInfo>>().Result;
            }
            return processInfo;
        }

        public ProcessInfo StartProcces(string path)
        {
            ProcessInfo processInfo = null;
            HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync($"https://localhost:7131/api/processes/command/executeCommand?Path={path}&CommandType=0",path).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<ProcessInfo>().Result;
            }
            return processInfo;
        }

        public ProcessInfo KillProcces(int id)
        {
            ProcessInfo processInfo = null;
            HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync($"https://localhost:7131/api/processes/command/executeCommand?ProcessId={id}&CommandType=1", id).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return processInfo = httpResponseMessage.Content.ReadAsAsync<ProcessInfo>().Result;
            }
            return processInfo;
        }

        public void Watch()
        {
            HttpResponseMessage httpResponseMessage = client.GetAsync("https://localhost:7131/api/processes/watch?webSocketCommandType=0").Result;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

    }
}
