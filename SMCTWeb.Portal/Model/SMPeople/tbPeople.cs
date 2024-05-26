using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SMCTPortal.Model.SMCV;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMCTPortal.Model.SMPeople
{
    //[Table("tbPeople",Schema ="SMCT.CITIZ.dbo")]
    public class tbPeople
    {
        public string _id { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
        public string citizenNo { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Relations { get; set; }
        public string ParentId { get; set; }
        public List <mdEducation > educationInfos { get; set; }
        public List <mdRelocation > relocationInfos { get; set; }
        public List <tbPeople > family { get; set; }
        public Resume resumeInfos { get; set; }
 
        
    }
}
