using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Dapper_Core_API.Contracts;
using Using_Dapper_Core_API.Entities;

namespace Using_Dapper_Core_API.Controllers
{
    [Route("api/Compaines")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository icompanyRepository;

        public CompaniesController(ICompanyRepository icompanyRepository)
        {
            this.icompanyRepository = icompanyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompaines()
        {
            try
            {
                var comp = await icompanyRepository.GetCompanies();
                return Ok(comp);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}",Name ="CompanyById")]
        public async Task<IActionResult> GetCompaines(int id)
        {
            try
            {
                var comp = await icompanyRepository.GetCompanies(id);
                if (comp == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(comp);
                }
               
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> insrtcompany([FromBody]CompanyForCreation companyFor)
        {
            try
            {
                //await icompanyRepository.insrtcompany(companyFor);
                //return Ok("Data insrted");

                var createdcompnay = await icompanyRepository.insrtcompany(companyFor);
                return CreatedAtRoute("CompanyById", new { createdcompnay.ID }, createdcompnay);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut ("{Id}")]
        public async Task<IActionResult> Update([FromBody] Update_Delete update_Delete,int id)
        {
            try
            {
                var getcom = await icompanyRepository.GetCompanies(id);
                if (getcom == null)
                {
                    return NotFound();
                }
                else
                {
                  await icompanyRepository.Updated(id, update_Delete);
                    return Ok("Updated Sucessss");
                    //return CreatedAtRoute("CompanyByDd", new { id }, updated);
                }


               
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete(  int id)
        {
            try
            {
                var getcom = await icompanyRepository.GetCompanies(id);
                if (getcom == null)
                {
                    return NotFound();
                }
                else
                await icompanyRepository.Delete(id);
                return Ok("Record Deleted" + id + "");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
