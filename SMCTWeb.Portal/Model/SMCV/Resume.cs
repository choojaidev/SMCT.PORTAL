using SMCTPortal.Model.SMPeople;
using System.Collections.Generic;

namespace SMCTPortal.Model.SMCV
{
    public class Resume
    {
        public string _id { get; set; }
        public string Status { get; set; }
        public string createDate { get; set; }
        public string updateDate { get; set; }
        public string citizenNo { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Provice { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string HashTag { get; set; }
        public string Avatar { get; set; }
        public List<mdSkill> SkillInfos { get; set; }
        public List<mdSocialMedia> SocialMediaInfos { get; set; }  
        public List<mdEducation> educationInfos { get; set; }
        public List<mdJobHistory> JobHistoryInfos { get; set; }
      
       
    }
    public class mdJobHistory {
        public string id { get; set; }
        public string citizenNo { get; set; }
        public string ComName { get; set; }
        public string Position { get; set; }
        public string Responsibility { get; set; }
        public string SinceYear { get; set; }
        public string ToYear { get; set; }
    }
  
    public class mdSoftSkillInfo { }
    public class mdSocialMedia {
        public string id { get; set; }
        public string citizenNo { get; set; }
        public string SocialName { get; set; }
        public string SocialURL { get; set; }
        public string SocialIcon { get; set; }
        
    }
    public class mdSkill
    {
        public string id { get; set; }
    
        public string citizenNo { get; set; }
        public string SkillName { get; set; }
        public string SkillValue { get; set; }
        public string URL { get; set; }
    }
    public class mdExportHist {
        public string expDTE { get; set; }
        public string expResult { get; set; }
        public string exp64Data { get; set; }
        
    }
}
