using System;
using System.Collections.Generic;
using System.Text;

namespace NeighborThrift4.Services
{
	public interface INotificationService
	{
		int Notify(Int64[] recipients, string message);
	}
}
