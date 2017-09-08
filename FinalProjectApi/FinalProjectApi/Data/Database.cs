using FinalProjectApi.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Data
{
    public class Database
    {
		static object locker = new object();
		readonly SQLiteConnection connection;

		public Database(string databaseName)
		{
			var dbdir = Directory.GetCurrentDirectory() + $"\\{databaseName}Database.sqlite";
			connection = new SQLiteConnection(dbdir);
		}

		public void CreateTable<T>()
		{
			lock (locker)
			{
				connection.CreateTable<T>();
			}
		}

		public int SaveItem<T>(T item)
		{
			lock (locker)
			{
				var id = ((BaseItem)(object)item).ID;
				if (id != 0)
				{
					connection.Update(item);
					return id;
				}
				else
				{
					return connection.Insert(item);
				}
			}
		}

		public void ExecuteQuery(string query, object[] args)
		{
			lock (locker)
			{
				connection.Execute(query, args);
			}
		}

		public List<T> Query<T>(string query, object[] args) where T : new()
		{
			lock (locker)
			{
				return connection.Query<T>(query, args);
			}
		}

		public IEnumerable<T> GetItems<T>() where T : new()
		{
			lock (locker)
			{
				return (from i in connection.Table<T>() select i).ToList();
			}
		}

		public int DeleteItem<T>(int id)
		{
			lock (locker)
			{
				return connection.Delete<T>(id);
			}
		}

		public int DeleteAll<T>()
		{
			lock (locker)
			{
				return connection.DeleteAll<T>();
			}
		}
	}
}
