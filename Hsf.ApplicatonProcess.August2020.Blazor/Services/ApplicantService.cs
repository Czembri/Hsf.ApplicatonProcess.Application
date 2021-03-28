using Hsf.ApplicatonProcess.August2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;



namespace Hsf.ApplicatonProcess.August2020.Blazor.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly HttpClient httpClient;

        public ApplicantService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Applicant> GetApplicant(int id)
        {
            return await httpClient.GetJsonAsync<Applicant>($"api/applicants/{id}");
        }

        public async Task<IEnumerable<Applicant>> GetApplicants()
        {
            return await httpClient.GetJsonAsync<Applicant[]>("api/applicants");
        }
    }
}
