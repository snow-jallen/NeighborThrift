using System;
using System.Collections.Generic;
using System.Text;
using NeighborThrift4.Data;

namespace NeighborThrift4.Services
{
	public interface IDataService
	{
		IEnumerable<ClothingItem> GetItems();
		void AddItem(ClothingItem item);
		void AddDonor(Donor donor);
		IEnumerable<Donor> GetDonors();
	}
}
