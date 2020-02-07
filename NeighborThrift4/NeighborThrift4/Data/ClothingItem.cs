using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace NeighborThrift4.Data
{
	public class ClothingItem
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Size { get; set; }
		public string Type { get; set; }
		public string Gender { get; set; }
		[NotNull]
		public decimal Price { get; set; }
		//public DateTime Arrived { get; set; }
		
		
		[ManyToOne]
		public Donor DonatedBy { get; set; }


		[ForeignKey(typeof(Donor))]
		public int DonorId { get; set; }
	}
}
