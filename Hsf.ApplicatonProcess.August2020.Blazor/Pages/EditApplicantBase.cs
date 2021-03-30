using Hsf.ApplicatonProcess.August2020.Blazor.Services;
using Hsf.ApplicatonProcess.August2020.Data;
using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Pages
{
    public class EditApplicantBase : ComponentBase
    {
        [Inject]
        public IApplicantService ApplicantService { get; set; }

        public List<string> Countries { get; set; }
        
        public Applicant Applicant { get; set; } = new Applicant();


        [Parameter]
        public string Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int applicantID);
            if(applicantID != 0)
            {
                Applicant = await ApplicantService.GetApplicant(int.Parse(Id));
            }
            else
            {
                Applicant = new Applicant();
            }

            var cc = new CountriesList();
            Countries = cc.GetNames();

        }
        protected async Task HandleValidSubmit()
        {
            Applicant result = null;
            if(Applicant.ID != 0)
            {
                result = await ApplicantService.UpdateApplicant(Applicant);
            }
            else
            {
                result = await ApplicantService.CreateApplicant(Applicant);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        protected async Task Delete_Click()
        {
            await ApplicantService.DeleteApplicant(Applicant.ID);
            NavigationManager.NavigateTo("/");
        }
    }
}
