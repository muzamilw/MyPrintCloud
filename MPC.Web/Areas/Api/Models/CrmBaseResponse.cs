using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class CrmBaseResponse
    {
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }
        public IEnumerable<CompanyContactRole> CompanyContactRoles { get; set; }
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }
        public IEnumerable<RegistrationQuestionDropDown> RegistrationQuestions { get; set; }
        public IEnumerable<CountryDropDown> Countries { get; set; }
        public IEnumerable<StateDropDown> States { get; set; }
        public IEnumerable<StoresListDropDown> StoresListDropDown { get; set; }

        public byte[] DefaultSpriteImage { get; set; }

        public string DefaultSpriteImageSource
        {
            get
            {
                if (DefaultSpriteImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(DefaultSpriteImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
        public string DefaultCompanyCss { get; set; }
        public long? DefaultCountryId { get; set; }
    }
}