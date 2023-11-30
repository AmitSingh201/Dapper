using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Dapper_Core_API.Contracts;
using Using_Dapper_Core_API.Context;
using Using_Dapper_Core_API.Entities;
using Dapper;
using Microsoft.Data.Sql;

namespace Using_Dapper_Core_API.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly DapperContext dapperContext;

        public CompanyRepository(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Company>> GetCompanies() {

            var query = "Select * from Companies";

            using (var connection = dapperContext.createconnection())
            {
                var tosave = await connection.QueryAsync<Company>(query);
                return tosave.ToList();
            }

        }

        public async Task<Company> GetCompanies(int id)
        {
            //var query2= "select * from Northwind.dbo.Companies where " + new { id }  +"";

            using (var connection = dapperContext.createconnection())
            {
                var getall = await connection.QueryFirstOrDefaultAsync<Company>("select * from Northwind.dbo.Companies where id=@ids ",new { ids = id});
                return getall;
            }
        }

        //public async Task insrtcompany(CompanyForCreation companyFor)
        //{
        //    using (var connection= dapperContext.createconnection())
        //    {
        //        var query2="insert into Northwind.dbo.Companies (Name,Address,Country) values(@Name,@Address,@Country)";
        //        var parameter = new DynamicParameters();
        //        parameter.Add("@Name", companyFor.Name, System.Data.DbType.String);
        //        parameter.Add("@Address", companyFor.Address, System.Data.DbType.String);
        //        parameter.Add("@Country", companyFor.Country, System.Data.DbType.String);
        //        var execute = await connection.ExecuteAsync(query2,parameter);




        //    }
        //}

        public async Task<Company> insrtcompany(CompanyForCreation companyFor)
        {
           
                var query2 = "insert into Northwind.dbo.Companies (Name,Address,Country) values(@Name,@Address,@Country)  " +"SELECT CAST(SCOPE_IDENTITY() as int)";
                var parameter = new DynamicParameters();
                parameter.Add("@Name", companyFor.Name, System.Data.DbType.String);
                parameter.Add("@Address", companyFor.Address, System.Data.DbType.String);
                parameter.Add("@Country", companyFor.Country, System.Data.DbType.String);
            using (var connection = dapperContext.createconnection())
            {
                var execute = await connection.QuerySingleAsync<int>(query2, parameter);

                var create = new Company
                {
                    ID = execute,
                    Name = companyFor.Name,
                    Address = companyFor.Address,
                    Country = companyFor.Country

                };

                return create;

            }
        }

        public async Task Updated(int id ,Update_Delete update_Delete)
        {
          
                var query="Update Northwind.dbo.Companies set Name=@Name, Address=@Address, Country=@Country  Where Id=@ID ";
                var parameter = new DynamicParameters();
                parameter.Add("@ID",id,System.Data.DbType.Int32);
                parameter.Add("@Name", update_Delete.Name, System.Data.DbType.String);
                parameter.Add("@Address", update_Delete.Address, System.Data.DbType.String);
                parameter.Add("@Country", update_Delete.country, System.Data.DbType.String);
            using (var conn = dapperContext.createconnection())
            {
               var respose= await conn.ExecuteAsync(query, parameter);

                //var response = new Company
                //{
                //    ID = respose,
                //    Name = update_Delete.Name,
                //    Address = update_Delete.Address,
                //    Country = update_Delete.country
                //};

                //return response;


            }
                
        }

        public async Task Delete(int id)
        {
            var query = "Delete from Northwind.dbo.Companies where id = "+id+"";
            using (var conn= dapperContext.createconnection())
            {
                await conn.ExecuteAsync(query, new { id});
            }
        }
    }
}
