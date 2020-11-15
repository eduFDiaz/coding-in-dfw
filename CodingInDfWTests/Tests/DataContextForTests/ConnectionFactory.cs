using System;
using coding.API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace coding.API.Tests.DataContextForTests
{
    public class ConnectionFactory : IDisposable
    {
         #region IDisposable Support  
        private bool disposedValue = false; // To detect redundant calls  
  
        public DataContext CreateContextForInMemory()  
        {  
            var option = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;  
  
            var context = new DataContext(option);  
            if (context != null)  
            {  
                context.Database.EnsureDeleted();  
                context.Database.EnsureCreated();  
            }  
  
            return context;  
        }  
  
        public DataContext CreateContextForSQLite()  
        {  
            var connection = new SqliteConnection("DataSource=:memory:");  
            connection.Open();  
  
            var option = new DbContextOptionsBuilder<DataContext>().UseSqlite(connection).Options;  
  
            var context = new DataContext(option);  
              
            if (context != null)  
            {  
                context.Database.EnsureDeleted();  
                context.Database.EnsureCreated();  
            }  
  
            return context;  
        }  
  
  
        protected virtual void Dispose(bool disposing)  
        {  
            if (!disposedValue)  
            {  
                if (disposing)  
                {  
                }  
  
                disposedValue = true;  
            }  
        }  
  
        public void Dispose()  
        {  
            Dispose(true);  
        }  
        #endregion  
    }
}