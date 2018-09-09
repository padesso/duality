using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Plugin;

namespace Duality.Components.Networking
{
	public class NetworkingComponent : Component, ICmpInitializable, ICmpUpdatable
	{
		[DontSerialize] public string IPAddress = "127.0.0.1";
		[DontSerialize] public int Port = 11011;
		[DontSerialize] private UdpSocketClient client;

		public void OnInit(InitContext context)
		{
			if(context == InitContext.Activate)
			{
				try
				{
					this.client = new UdpSocketClient();
					this.client.ConnectAsync("127.0.0.1", this.Port);
				}
				catch (Exception ex)
				{
					Log.Core.WriteError("Error creating Server: " + ex.Message);
				}
			}
		}

		public void OnShutdown(ShutdownContext context)
		{
			//TODO
		}

		public void OnUpdate()
		{
			byte[] sampleData = Encoding.UTF8.GetBytes("test data");
			this.client.SendAsync(sampleData);
		}
	}
}
