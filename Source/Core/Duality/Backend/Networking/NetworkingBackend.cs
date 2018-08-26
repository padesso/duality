using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
