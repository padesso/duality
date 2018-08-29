using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;

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
			get { return "UDP Networking Backend"; }
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
				int listenPort = 11011;
				UdpSocketReceiver receiver = new UdpSocketReceiver();

				receiver.MessageReceived += (sender, args) =>
				{
				// get the remote endpoint details and convert the received data into a string
				string from = string.Format("{0}:{1}", args.RemoteAddress, args.RemotePort);
					string data = Encoding.UTF8.GetString(args.ByteData, 0, args.ByteData.Length);

					Log.Core.Write("{0} - {1}", from, data);
				};

				// listen for udp traffic on listenPort
				receiver.StartListeningAsync(listenPort);
			}
			catch(Exception ex)
			{
				Log.Core.WriteError("Error creating UDP Server: " + ex.Message);
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
