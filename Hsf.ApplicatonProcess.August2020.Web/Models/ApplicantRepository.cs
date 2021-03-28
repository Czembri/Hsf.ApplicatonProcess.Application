using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Web.Models
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly AppDbContext appDbContext;
        public ApplicantRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Applicant> AddApplicant(Applicant applicant)
        {
            var result = await appDbContext.Applicants.AddAsync(applicant);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Applicant> DeleteApplicantAsync(int applicantID)
        {
            var result = await appDbContext.Applicants.FirstOrDefaultAsync(e => e.ID == applicantID);
            if (result != null)
            {
                appDbContext.Applicants.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Applicant> GetApplicant(int applicantID)
        {
            return await appDbContext.Applicants
                .FirstOrDefaultAsync(e => e.ID == applicantID);
        }
        public async Task<Applicant> GetApplicantByEmail(string email)
        {
            return await appDbContext.Applicants.FirstOrDefaultAsync(e => e.EmailAddress == email);
        }


        public async Task<IEnumerable<Applicant>> GetApplicants()
        {
            return await appDbContext.Applicants.ToListAsync();
        }

        public async Task<IEnumerable<Applicant>> Search(string name)
        {
            IQueryable<Applicant> query = appDbContext.Applicants;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Applicant> UpdateApplicant(Applicant applicant)
        {
            var result = await appDbContext.Applicants.FirstOrDefaultAsync(e => e.ID == applicant.ID);
            if (result != null)
            {
                result.Name = applicant.Name;
                result.FamilyName = applicant.FamilyName;
                result.Address = applicant.Address;
                result.Age = result.Age;
                result.EmailAddress = applicant.EmailAddress;
                result.CountryOfOrigin = result.CountryOfOrigin;
                result.Hired = result.Hired;

                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
