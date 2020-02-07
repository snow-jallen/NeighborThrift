using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NeighborThrift4.Data
{
	public class Donor
	{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		[OneToMany(CascadeOperations = CascadeOperation.All)]
		public List<ClothingItem> DonatedItems { get; set; }
	}
}
