using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duality.Components.Networking
{
	public class NetworkingComponent : Component
	{
		[DontSerialize] public string IPAddress = "Test";
	}
}
