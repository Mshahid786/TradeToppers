using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Repository
{
    public class AccountRepository : IRepository
    {
        private readonly string _connectionString;
        public AccountRepository(IOptions<ApplicationDbContext> context)
        {
            _connectionString = context.Value.ConnectionString;
        }
        public Task<IEnumerable<GLAccount>> getAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
