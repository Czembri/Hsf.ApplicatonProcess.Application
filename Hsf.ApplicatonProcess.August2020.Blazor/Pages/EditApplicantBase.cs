using Hsf.ApplicatonProcess.August2020.Blazor.Services;
using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Pages
{
    public class EditApplicantBase : ComponentBase
    {
        [Inject]
        public IApplicantService ApplicantService { get; set; }

        public Applicant Applicant { get; set; } = new Applicant();

        [Parameter]
        public string Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Applicant = await ApplicantService.GetApplicant(int.Parse(Id));
        }
        protected async Task HandleValidSubmit()
        {
            var result = await ApplicantService.UpdateApplicant(Applicant);
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
