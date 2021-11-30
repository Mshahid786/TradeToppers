using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechBiteERP.Model.Models;

namespace TechBiteERP.Data
{
    public interface IRepository
    {
        Task<IEnumerable<GLAccount>> getAccounts();
    }
}
