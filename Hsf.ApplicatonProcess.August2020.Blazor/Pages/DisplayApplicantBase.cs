using Hsf.ApplicatonProcess.August2020.Domain;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Pages
{
    public class DisplayApplicantBase : ComponentBase
    {
        [Parameter]
        public Applicant Applicant { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        [Parameter]
        public EventCallback<bool> OnApplicantSelected { get; set; }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            await OnApplicantSelected.InvokeAsync((bool)e.Value);
        }
    }
}
