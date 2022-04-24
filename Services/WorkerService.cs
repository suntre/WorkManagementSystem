using WorkManagementSystem.Interfaces;
using WorkManagementSystem.Entities;
using WorkManagementSystem.Models;
using WorkManagementSystem.Data;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WorkManagementSystem.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace WorkManagementSystem.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly MyAppData _dbcontext;
        private readonly IMapper _mapper;
        private readonly IOptions<HashingOptions> _hashingOptions;
        private readonly IConfiguration _configuration;

        public WorkerService(MyAppData dbcontext, IMapper mapper, IOptions<HashingOptions> options, IConfiguration configuration)
        {
            this._dbcontext = dbcontext;
            this._mapper = mapper;
            this._hashingOptions = options;
            this._configuration = configuration;
        }
        public IEnumerable<WorkerListDTO> GetWorkers()
        {
            var workerResults = _dbcontext.Worker
                .Include(w => w.role)
                .ToList();
            var workers = _mapper.Map<List<WorkerListDTO>>(workerResults);
            return workers;
        }
        public WorkerListDTO GetWorkerById(int id)
        {
            var workerResult = _dbcontext.Worker
                .Include(w => w.role)
                .SingleOrDefault(x => x.id == id);
            var worker = _mapper.Map<WorkerListDTO>(workerResult);
            return worker;
        }

        public IEnumerable<WorkerListDTO> GetByRole(string roleName)
        {
            var role = _dbcontext.Role.SingleOrDefault(r => r.name == roleName);
            if(role == null) return null;
            var workersResult = _dbcontext.Worker.Where(x => x.role.id == role.id)
                .Include(w => w.role)
                .ToList();
            var workers = _mapper.Map<IEnumerable<WorkerListDTO>>(workersResult);
            return workers;
        }

        public int CreateWorker(CreateWorkerDTO worker)
        {
            var tmp = _dbcontext.Worker.SingleOrDefault(x => x.login == worker.login);
            if (tmp != null) return -1;
            var hasher = new PasswordHasher(_hashingOptions);
            var role = _dbcontext.Role.SingleOrDefault(x => x.name == worker.roleName);
            if (role == null) return -2;
            var newWorker = _mapper.Map<WorkerEntity>(worker);
            newWorker.role = role;
            newWorker.password = hasher.Hash(worker.password);
            _dbcontext.Worker.Add(newWorker);
            _dbcontext.SaveChanges();
            return 0;
        }
        public int UpdateWorker(WorkerEntity worker)
        {
            _dbcontext.Update(worker);
            _dbcontext.SaveChanges();
            return 1;
        }
        public int DeleteWorker(int id)
        {
            var worker = _dbcontext.Worker.SingleOrDefault(x => x.id == id);
            if (worker == null) return -1;
            _dbcontext.Remove(worker);
            _dbcontext.SaveChanges();
            return 0;
        }
        public string Login(UserLoginDTO userToLogin)
        {
            var user = _dbcontext.Worker
                .Include(u => u.role)
                .FirstOrDefault(x => x.login == userToLogin.login);
            if (user != null)
            {
                var hasher = new PasswordHasher(_hashingOptions);
                bool loggedIn = hasher.Check(user.password, userToLogin.password);
                if (loggedIn)
                {
                    var tmp = _mapper.Map<WorkerEntity>(user);
                    string token = CreateToken(tmp);
                    return token;
                }
            }
            return string.Empty;
        }

        public string CreateToken(WorkerEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.login),
                new Claim(ClaimTypes.Role, user.role.name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

