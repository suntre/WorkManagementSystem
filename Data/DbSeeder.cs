using Microsoft.Extensions.Options;
using WorkManagementSystem.Authentication;
using WorkManagementSystem.Entities;


namespace WorkManagementSystem.Data
{
    public class DbSeeder
    {
        MyAppData _dbcontext;
        private readonly IOptions<HashingOptions> _hashingOptions;
        public DbSeeder(MyAppData dbcontext, IOptions<HashingOptions> options)
        {
            _dbcontext = dbcontext;
            _hashingOptions = options;
        }

        public void Seed()
        {
            if(_dbcontext.Database.CanConnect())
            {
                if(!_dbcontext.Role.Any())
                {
                    var roles = GetRoles();
                    _dbcontext.Role.AddRange(roles);
                    _dbcontext.SaveChanges();
                }
                if(!_dbcontext.Worker.Any())
                {
                    var workers = GetWorkers();
                    _dbcontext.Worker.AddRange(workers);
                    _dbcontext.SaveChanges();
                }
            }
            
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role(){name = "Superior"},
                new Role(){name = "Manager"},
                new Role(){name = "Programmer"}
            };
            return roles;
        }

        private IEnumerable<WorkerEntity> GetWorkers()
        {
            var hasher = new PasswordHasher(_hashingOptions);

            var workers = new List<WorkerEntity>()
            {
                new WorkerEntity()
                {
                    name = "John",
                    lastName = "Smith",
                    role = _dbcontext.Role.Single(r => r.name == "Superior"),
                    login = "josm",
                    password = hasher.Hash("josmPassword")
                },
                new WorkerEntity()
                {
                    name = "Jack",
                    lastName = "Petherson",
                    role = _dbcontext.Role.Single(r => r.name == "Manager"),
                    login = "jape",
                    password = hasher.Hash("japePassword")
                },
                new WorkerEntity()
                {
                    name = "Tom",
                    lastName = "Harwey",
                    role = _dbcontext.Role.Single(r => r.name == "Programmer"),
                    login = "toha",
                    password = hasher.Hash("tohaPassword")
                }
            };

            return workers;
        }

    }
}
