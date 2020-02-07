using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NeighborThrift4.Data;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace NeighborThrift4.Services
{
	public class SqliteDataService : IDataService
	{
		public SqliteDataService()
		{
			try
			{
				var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
				database = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.Create);
				database.CreateTable<ClothingItem>();
				database.CreateTable<Donor>();
			}
			catch(Exception ex)
			{

			}
		}

		private SQLiteConnection database;

		public void AddItem(ClothingItem item)
		{
			database.Insert(item);
		}

		public IEnumerable<ClothingItem> GetItems()
		{
			return database.Table<ClothingItem>().ToList();
		}

		public void AddDonor(Donor donor)
		{
			database.Insert(donor);
		}

		public IEnumerable<Donor> GetDonors()
		{
			return database.GetAllWithChildren<Donor>();
		}
	}
}
