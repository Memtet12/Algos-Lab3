using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseCore
    {
        public class ApplicationContext : DbContext
        {
            public DbSet<PolishNotationData> PolishNotationData { get; set; } = null!;
            public DbSet<OperationForDataStruct> OperationForDataStruct { get; set; } = null!;
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                string baseFolder = AppDomain.CurrentDomain.BaseDirectory + "datasets.db";
                optionsBuilder.UseSqlite($"Data Source={baseFolder.Replace("\\bin\\Debug\\net6.0-windows", "")}");
            }

        }
    }
}
