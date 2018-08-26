using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duality.Backend
{
	public interface INetworkingBackend : IDualityBackend
	{
		//TODO
		void UpdateWorldSettings();
	}
}
