using Hsf.ApplicatonProcess.August2020.Blazor.Services;
using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Pages
{
    public class ApplicantsList : ComponentBase
    {
        [Inject]
        public IApplicantService ApplicantService { get; set; }

        public bool ShowFooter { get; set; } = true;
        public IEnumerable<Applicant> Applicants { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Applicants = (await ApplicantService.GetApplicants()).ToList();
        }

        protected int ApplicantsSelectedCount { get; set; } = 0;

        protected void ApplicantSelectedChange(bool isSelected)
        {
            if (isSelected)
            {
                ApplicantsSelectedCount++;
            }
            else
            {
                ApplicantsSelectedCount--;
            }
        }
    }
}
