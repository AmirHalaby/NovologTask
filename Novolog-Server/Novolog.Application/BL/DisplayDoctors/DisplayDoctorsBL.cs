using Microsoft.Extensions.Logging;
using Novolog.Application.Dtos;
using Newtonsoft.Json;


namespace Novolog.Application.BL.DisplayDoctors
{
    internal class DisplayDoctorsBL: IDisplayDoctorsBL
    {
        private readonly ILogger<DisplayDoctorsBL> _logger;

        IEnumerable<DoctorsDto> doctors = new List<DoctorsDto>();

        Dictionary<string, Dictionary<string, string>> languages =
           new Dictionary<string, Dictionary<string, string>>();

        public DisplayDoctorsBL(ILogger<DisplayDoctorsBL> logger)
        {
            _logger = logger;
        }
        public IList<ResponseDisplayDoctors> GetDoctorsList()
        {
            IList<ResponseDisplayDoctors> responseDisplayDoctors = new List<ResponseDisplayDoctors>();
            try
            {
                _logger.LogInformation("GetDoctorsListBL Start");

                // Get only active doctors from json file with descending OrderBy serviceRate,totalRatings and then OrderBy promotionLevel.
                GetDoctorsFromJsonFile();
                _logger.LogInformation("GetDoctorsFromJsonFile success");

                // Get languages from Json file and save them in languages dictionary
                GetLanguagesFromJsonFile();
                _logger.LogInformation("GetLanguagesFromJsonFile success");

                foreach (var doctor in doctors)
                {
                     responseDisplayDoctors.Add(
                         new ResponseDisplayDoctors(
                             doctor.fullName, // FullName 
                            //  Handle Phone vlidation Phone number must be with format xx-xxxxxxx or xxx-xxxxxxx
                             PhoneValidation(doctor.phones.FirstOrDefault().number), // PhoneNumber                       
                             GetlanguageName(doctor.languageIds), // LanguagesName
                             doctor.reviews.serviceRate // ServiceRate
                             )
                         );                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("GetDoctorsListBL Exception");
                _logger.LogInformation(ex.ToString());
                throw ex;
            }

            _logger.LogInformation("GetDoctorsListBL Finish");
            return responseDisplayDoctors;
           
        }

        
        public List<string> GetlanguageName(List<string> languageIds)
        {
            List<string> result = new List<string>();
            foreach (var language in languageIds)
            {
                if(languages["language"].ContainsKey(language))
                    result.Add(languages["language"][language]);
            }
            return result;
        }
        // Phone Heandle: must be with Format xxx-xxxxxxx or xx-xxxxxxx
        public string PhoneValidation(string phoneNumber)
        {
            if (!phoneNumber.Contains(Constants.UNDERSCORE))
            {
                if (phoneNumber.Length == Constants.MOBILE_NUMBER_LENGHT)
                {
                    return phoneNumber.Substring(0, 3) + Constants.UNDERSCORE.ToString() + phoneNumber.Substring(3, 7);
                }
                else if (phoneNumber.Length == Constants.PHONE_NUMBER_LENGHT)
                {
                    return  phoneNumber.Substring(0, 2) + Constants.UNDERSCORE.ToString() + phoneNumber.Substring(2, 7);
                }
            }
            return phoneNumber;           
        }

        public void GetDoctorsFromJsonFile()
        {
            using StreamReader doctorsReader = new(Constants.DOCTORS_JSON_PATH);
            var json = doctorsReader.ReadToEnd();

            doctors = JsonConvert.DeserializeObject<List<DoctorsDto>>(json)
                .Where(x => x.isActive == true && x.promotionLevel <= 5)
                .OrderByDescending(p => p.reviews.serviceRate)
                .ThenByDescending(p => p.reviews.totalRatings)
                .ThenBy(p => p.promotionLevel);
        }

        public void GetLanguagesFromJsonFile()
        {
            using StreamReader languagesReader = new(Constants.LANGUAGE_JSON_PATH);
            var languagesjson = languagesReader.ReadToEnd();
            languages = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(languagesjson);
        }
    }
}

