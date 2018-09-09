using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;

namespace Duality.Backend.Networking
{
	public class NetworkingBackend : INetworkingBackend
	{
		private static NetworkingBackend activeInstance = null;
		internal static NetworkingBackend ActiveInstance
		{
			get { return activeInstance; }
		}

		string IDualityBackend.Id
		{
			get { return "NetworkingBackend"; }
		}
		string IDualityBackend.Name
		{
			get { return "Networking Backend"; }
		}
		int IDualityBackend.Priority
		{
			get { return int.MinValue; }
		}

		bool IDualityBackend.CheckAvailable()
		{
			return true;
		}
		void IDualityBackend.Init()
		{
			//TESTING: https://github.com/rdavisau/sockets-for-pcl
			try
			{
				int listenPort = 11000;
				TcpSocketListener listener = new TcpSocketListener();

				// when we get connections, read byte-by-byte from the socket's read stream
				listener.ConnectionReceived += (sender, args) =>
				{
					ITcpSocketClient client = args.SocketClient;

					int bytesRead = -1;
					byte[] buf = new byte[1];

					while (bytesRead != 0)
					{
						bytesRead = args.SocketClient.ReadStream.Read(buf, 0, 1);
						if (bytesRead > 0)
							Debug.WriteLine(buf[0]);
					}
				};

				// bind to the listen port across all interfaces
				listener.StartListeningAsync(listenPort);
			}
			catch(Exception ex)
			{
				Log.Core.WriteError("Error creating TCP Server: " + ex.Message);
			}

			Log.Core.WriteWarning("NetworkingBackend initialized.");
			activeInstance = this;
		}
		void IDualityBackend.Shutdown()
		{
			if (activeInstance == this)
				activeInstance = null;
		}

		//TODO
		void INetworkingBackend.UpdateWorldSettings() { }	
	}
}
