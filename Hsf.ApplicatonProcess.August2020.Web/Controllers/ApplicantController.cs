using Hsf.ApplicatonProcess.August2020.Domain;
using Hsf.ApplicatonProcess.August2020.Web.ConfigurationSetup;
using Hsf.ApplicatonProcess.August2020.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IApplicantRepository applicantRepository;
        private int Code500 { get; set; } = StatusCodes.Status500InternalServerError;
        private int Code200 { get; set; } = StatusCodes.Status200OK;
        private int Code400 { get; set; } = StatusCodes.Status400BadRequest;

        private IConfigurationSection Section { get; set; }

        public ApplicantsController(IApplicantRepository applicantRepository, IConfiguration _config)
        {
            this.applicantRepository = applicantRepository;
            configuration = _config;
            Section = configuration.GetSection("MessagesConfig");
        }
        [HttpGet]
        public async Task<ActionResult> GetApplicants()
        {
            try
            {
                Log.Debug($"{Section.GetValue<string>("DebugAPIget")}{nameof(GetApplicants)}\nCode: [{Code200}]");
                return Ok(await applicantRepository.GetApplicants());
            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Applicant>>> Search(string name)
        {
            try
            {
                Log.Debug($"{Section.GetValue<string>("DebugAPIget")}{nameof(Search)}\nCode: [{Code200}]");
                var result = await applicantRepository.Search(name);
                if (result.Any())
                {
                    Log.Information($"{Section.GetValue<string>("Search")}\nResult:[{result}]");
                    return Ok(result);
                }
                Log.Error($"{Section.GetValue<string>("NotFound")}[object={nameof(Applicant.Name)}; name={name}]");
                return NotFound();
            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetAplicant(int id)
        {
            try
            {
                Log.Debug($"{Section.GetValue<string>("DebugAPIget")}{nameof(GetAplicant)} - id={id}\nCode: [{Code200}]");
                var result = await applicantRepository.GetApplicant(id);

                if (result == null)
                {
                    Log.Error($"{Section.GetValue<string>("NotFound")}[object={nameof(GetAplicant)}; id={id}]");
                    return NotFound();
                }
                Log.Information($"[object=Applicant; id={id}] has been retreived\nResult: [{result}]");
                return result;
            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<Applicant>> CreateApplicant(Applicant applicant)
        {
            try
            {
                if (applicant == null)
                {
                    Log.Error($"{Section.GetValue<string>("RetreiveFromDbError")}\nCode: [{Code400}]");
                    return BadRequest();
                }
                //var app = applicantRepository.GetApplicantByEmail(applicant.EmailAddress);
                //if (app != null)
                //{
                //    ModelState.AddModelError("email", "Applicant email already in use");
                //    return BadRequest(ModelState);
                //}
                var createdApplicant = await applicantRepository.AddApplicant(applicant);
                Log.Information($"Object created: [object=Applicant; id={createdApplicant.ID}; name={createdApplicant.Name}; \naddress={createdApplicant.Address}; e-mail={createdApplicant.EmailAddress}]");
                Log.Debug($"Created object: [{createdApplicant}]");
                return CreatedAtAction(nameof(GetAplicant), new { id = createdApplicant.ID }, createdApplicant);
            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Applicant>> UpdateApplicant(int id, Applicant applicant)
        {
            try
            {
                var applicantToUpdate = await applicantRepository.GetApplicant(id);

                if (applicantToUpdate == null)
                {
                    Log.Error($"{Section.GetValue<string>("NotFound")}[object=Applicant; id={id}]");
                    return NotFound($"Applicant with id= {id} not found");
                }
                Log.Information($"[object=Applicant; id={id}] has been updated");
                Log.Debug($"Updated object: [{applicantToUpdate}]");
                return await applicantRepository.UpdateApplicant(applicant);

            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, Section.GetValue<string>("RetreiveFromDbError"));
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Applicant>> DeleteApplicant(int id)
        {
            try
            {
                var applicantToDelete = await applicantRepository.GetApplicant(id);
                if (applicantToDelete == null)
                {
                    Log.Error($"{Section.GetValue<string>("NotFound")}[object=Applicant; id={id}]");
                    return NotFound($"Applicant with id= {id} not found");
                }
                Log.Information($"[object=Applicant; id={id}] has been deleted");
                Log.Debug($"Deleted object: [{applicantToDelete}]");
                return await applicantRepository.DeleteApplicantAsync(id);
            }
            catch (Exception err)
            {
                Log.Error($"Exception occured: [{err}]\nStatusCode:{Code500}");
                return StatusCode(Code500, $"Exception occured: {err}\n{Section.GetValue<string>("DeletingError")}");
            }
        }
    }
}
