using Hsf.ApplicatonProcess.August2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Services
{
    public interface IApplicantService
    {
        Task<IEnumerable<Applicant>> GetApplicants();
        Task<Applicant> GetApplicant(int id);
        Task<Applicant> UpdateApplicant(Applicant applicant);
    }
}
