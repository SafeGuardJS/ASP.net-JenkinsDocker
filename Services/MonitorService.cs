using WebApiASP.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace WebApiASP.Services
{
    public class MonitorService
    {
        private readonly IMongoCollection<Monitor> _monitor;

        public MonitorService(IMonitorstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _monitor = database.GetCollection<Monitor>(settings.MonitorsCollectionName);
        }

        public List<Monitor> Get() =>
            _monitor.Find(monitor => true).ToList();

        public Monitor Get(string id) =>
            _monitor.Find<Monitor>(monitor => monitor.Id == id).FirstOrDefault();

        public Monitor Create(Monitor monitor)
        {
            _monitor.InsertOne(monitor);
            return monitor;
        }

        public void Update(string id, Monitor monitorIn) =>
            _monitor.ReplaceOne(monitor => monitor.Id == id, monitorIn);

        public void Remove(Monitor monitorIn) =>
            _monitor.DeleteOne(monitor => monitor.Id == monitorIn.Id);

        public void Remove(string id) =>
            _monitor.DeleteOne(monitor => monitor.Id == id);
    }
}