using System;
using System.ComponentModel.DataAnnotations;

namespace Hsf.ApplicatonProcess.August2020.Domain
{
    public class Applicant
    {
        public int ID { get; set; }
        [MinLength(5)]
        public string Name { get; set; }
        [MinLength(5)]
        public string FamilyName { get; set; }
        [MinLength(10)]
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Range(20,60)]
        public int Age { get; set; }
        [Required]
        public bool Hired { get; set; }
    }
}
