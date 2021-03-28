using Hsf.ApplicatonProcess.August2020.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Web.Models
{
    public interface IApplicantRepository
    {

        Task<IEnumerable<Applicant>> GetApplicants();
        Task<IEnumerable<Applicant>> Search(string name);
        Task<Applicant> GetApplicant(int applicantID);
        Task<Applicant> GetApplicantByEmail(string email);
        Task<Applicant> AddApplicant(Applicant applicant);
        Task<Applicant> UpdateApplicant(Applicant applicant);
        Task<Applicant> DeleteApplicantAsync(int applicantID);
    }
}
