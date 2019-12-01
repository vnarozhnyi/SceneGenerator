using Microsoft.AspNetCore.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SceneGenerator.DataRepository
{
    public class DataProvider
    {
        public static DataProvider GetInstance() => new DataProvider();

        private readonly ConnectionContext _context;

        private DataProvider()
        {
            _context = new DefaultConnectionContext();
        }

        public T GetDataSet<T>(long id) =>
            (T)Convert.ChangeType(_context.Items.AsQueryable().ElementAtOrDefault((int)id), typeof(T));

        public long SettDataSet<T>(T value)
        {
            var d = new Dictionary<object, object>
            {
                { _context.Items.Count + 1, value }
            };

            _context.Items.Add(d.ToList().First());

            return _context.Items.Count;
        }
    }
}
