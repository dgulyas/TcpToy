using System;
using System.Net.Sockets;

namespace TcpClient
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Client:");
			var message = "IAmDataFromClient";
			try
			{
				using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("localhost", 9999))
				{
					var data = System.Text.Encoding.ASCII.GetBytes(message);

					using (NetworkStream stream = client.GetStream())
					{
						Console.WriteLine($"Client: Sending '{message}' to server.");
						stream.Write(data, 0, data.Length);

						// Buffer to store the response bytes.
						data = new byte[256];

						// Read the first batch of the TcpServer response bytes.
						int bytes = stream.Read(data, 0, data.Length);
						var responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
						Console.WriteLine($"Client: Received '{responseData}'");
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("\n Press Enter to continue...");
			Console.Read();
		}

	}
}
