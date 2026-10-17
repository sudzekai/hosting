using Sudzekai.Hosting.WebHost.Abstractions;
using System.Net;
using System.Text;

namespace Sudzekai.Hosting.WebHost.WebHosts
{
    internal class Listener
    {
        public Listener(string host, int port, IReadOnlyCollection<IHttpHandler> handlers)
        {
            _listener = new();
            _listener.Prefixes.Add($"http://{host}:{port}/");
            _listener.Start();
        }

        private HttpListener _listener;
    
        public async Task ListenAsync()
        {
            while (true)
            {
                HttpListenerContext context = _listener.GetContext();

                // Формируем ответ
                string responseString = "<HTML><BODY>Hello world!</BODY></HTML>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                // Отправляем
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
        }
    }
}
