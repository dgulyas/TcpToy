using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Server:");

			TcpListener server = new TcpListener(IPAddress.Any, 9999);
			// we set our IP address as server's address, and we also set the port: 9999

			server.Start();

			while (true)
			{
				TcpClient client = server.AcceptTcpClient();

				NetworkStream ns = client.GetStream();

				Console.WriteLine($"Server: Sending 'IAmDataFromServer' to server.");
				var response = Encoding.Default.GetBytes("IAmDataFromServer");
				ns.Write(response, 0, response.Length);     //sending the message

				while (client.Connected)  //while the client is connected, we look for incoming messages
				{
					byte[] msg = new byte[1024];     //the messages arrive as byte array
					ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
					Console.WriteLine("Server: Recieved '" +  Encoding.Default.GetString(msg) + "'"); //now , we write the message as string
				}
			}

		}
	}
}
