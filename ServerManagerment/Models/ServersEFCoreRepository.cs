using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ServerManagement.Data;


namespace ServerManagement.Models
{
    public class ServersEFCoreRepository : IServersEFCoreRepository
    {
        private readonly IDbContextFactory<ServerManagementContext> contextFactory;
        public ServersEFCoreRepository(IDbContextFactory<ServerManagementContext> newContextFactory)
        {
            this.contextFactory = newContextFactory;
        }

        public void AddServer(Server server)
        {
            using var db = contextFactory.CreateDbContext();
            db.Servers.Add(server);//In memory
            db.SaveChanges();//Call the database
        }

        public List<Server> GetServers()
        {
            using var db = contextFactory.CreateDbContext();
            return db.Servers.ToList();
        }

        public List<Server> GetServersByCity(string cityName)
        {
            using var db = contextFactory.CreateDbContext();
            return db.Servers.Where(x => x.City != null && x.City.ToLower().IndexOf(cityName.ToLower()) >= 0).ToList();
        }

        public Server? GetServerById(int id)
        {
            using var db = contextFactory.CreateDbContext();
            var server = db.Servers.Find(id);
            if (server is not null) return server;

            return new Server();
        }

        public void UpdateServer(int serverId, Server server)
        {
            if (server == null) throw new ArgumentNullException(nameof(server));
            if (serverId != server.ServerId) return;

            using var db = contextFactory.CreateDbContext();
            var serverToUpdate = db.Servers.Find(serverId);
            if (serverToUpdate is not null)
            {
                serverToUpdate.IsOnline = server.IsOnline;
                serverToUpdate.Name = server.Name;
                serverToUpdate.City = server.City;
                db.SaveChanges();
            }
        }

        public void DeleteServer(int serverId)
        {
            using var db = contextFactory.CreateDbContext();
            var server = db.Servers.Find(serverId);
            if (server is null) return;

            db.Servers.Remove(server);
            db.SaveChanges();

        }

        public List<Server> SearchServers(string serverFilter)
        {
            using var db = contextFactory.CreateDbContext();
            return db.Servers.Where(x =>
                x.Name != null &&
                x.Name.ToLower().IndexOf(serverFilter.ToLower()) >= 0).ToList();
        }
    }
}
