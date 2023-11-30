using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Dapper_Core_API.Entities;

namespace Using_Dapper_Core_API.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();

        public Task<Company> GetCompanies(int id);

        //this is for normal inserting using exexuteasync
        //public Task insrtcompany(CompanyForCreation companyFor);

        // in this we are sending the link while inserting
        public Task<Company> insrtcompany(CompanyForCreation companyFor);

        //public Task<Company> Updated(int id, Update_Delete update_Delete);
        public Task Updated(int id, Update_Delete update_Delete);

        public Task Delete(int id);
    }
}
