using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FormsPoc
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private MediaFile _data;

        string JsonString = "{\"answers\":[{\"question_id\":1,\"question_text\":\"Actual Leakage Current? - values to be filled in mA\",\"response\":\"0\",\"response_type\":\"number\",\"required\":1,\"tempComments\":null,\"comments\":[{\"note\":\"Not Working\",\"img\":null}],\"isComment\":false,\"commentsOrg\":[{\"note\":\"Not Working\",\"img\":null}],\"CommentImgOrg\":\"comment.png\",\"CommentImg\":\"comment.png\",\"options\":null,\"question_options\":null,\"radio_true\":false,\"is_Entry\":false,\"is_Number\":true,\"is_Picker\":false,\"is_Signature\":false,\"yesSelected\":false,\"noSelected\":false,\"naSelected\":false,\"is_Radio\":false,\"is_Date\":false,\"response_selected\":{\"id\":null,\"result\":null},\"radio_response_selected\":\"0\",\"qid_response_type\":\"1_number\",\"is_Required\":true,\"StartDate\":\"2019-05-07T00:00:00+05:30\",\"dateresponse\":\"\"},{\"question_id\":2,\"question_text\":\"Date of testing?\",\"response\":\"05/07/2019\",\"response_type\":\"date\",\"required\":1,\"tempComments\":null,\"comments\":null,\"isComment\":false,\"commentsOrg\":null,\"CommentImgOrg\":null,\"CommentImg\":\"uncomment.png\",\"options\":null,\"question_options\":null,\"radio_true\":false,\"is_Entry\":false,\"is_Number\":false,\"is_Picker\":false,\"is_Signature\":false,\"yesSelected\":false,\"noSelected\":false,\"naSelected\":false,\"is_Radio\":false,\"is_Date\":true,\"response_selected\":{\"id\":null,\"result\":null},\"radio_response_selected\":\"05/07/2019\",\"qid_response_type\":\"2_date\",\"is_Required\":true,\"StartDate\":\"2019-05-07T00:00:00+05:30\",\"dateresponse\":\"05/07/2019\"},{\"question_id\":3,\"question_text\":\"Test remarks? - Free text\",\"response\":\"Not working\",\"response_type\":\"simple text\",\"required\":1,\"tempComments\":null,\"comments\":null,\"isComment\":false,\"commentsOrg\":null,\"CommentImgOrg\":null,\"CommentImg\":\"uncomment.png\",\"options\":null,\"question_options\":null,\"radio_true\":false,\"is_Entry\":true,\"is_Number\":false,\"is_Picker\":false,\"is_Signature\":false,\"yesSelected\":false,\"noSelected\":false,\"naSelected\":false,\"is_Radio\":false,\"is_Date\":false,\"response_selected\":{\"id\":null,\"result\":null},\"radio_response_selected\":\"Not working\",\"qid_response_type\":\"3_simple text\",\"is_Required\":true,\"StartDate\":\"2019-05-07T00:00:00+05:30\",\"dateresponse\":\"\"},{\"question_id\":4,\"question_text\":\"Available At?\",\"response\":\"At site\",\"response_type\":\"select\",\"required\":1,\"tempComments\":null,\"comments\":null,\"isComment\":false,\"commentsOrg\":null,\"CommentImgOrg\":null,\"CommentImg\":\"uncomment.png\",\"options\":null,\"question_options\":[{\"id\":\"1\",\"result\":\"In store\"},{\"id\":\"2\",\"result\":\"At site\"}],\"radio_true\":false,\"is_Entry\":false,\"is_Number\":false,\"is_Picker\":true,\"is_Signature\":false,\"yesSelected\":false,\"noSelected\":false,\"naSelected\":false,\"is_Radio\":false,\"is_Date\":false,\"response_selected\":{\"id\":null,\"result\":null},\"radio_response_selected\":\"At site\",\"qid_response_type\":\"4_select\",\"is_Required\":true,\"StartDate\":\"2019-05-07T00:00:00+05:30\",\"dateresponse\":\"\"}]}";

        public MainPage()
        {
            InitializeComponent();


            List <AssetMetaDataQuestion> list = Convert(JsonConvert.DeserializeObject<Questions>(JsonString));
            var json = JsonConvert.SerializeObject(list);

            Problem problem = new Problem();
             string givenSentence = "I tried to use the back navigation by overriding OnBackButtonPressed, but somehow it wasn't get called at all. I am using the ContentPage and the latest 1.4.2 release";
             string matchSentence = "I tried to $ the back";
            problem.RemoveWordFromString("Calendar", 2, 2);
            problem.FindMatch(givenSentence, matchSentence);
        }


        //async void AddPhotoClicked(object sender, EventArgs e)
        //{
            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    await DisplayAlert("No Camera", ":( No camera available.", "OK");
            //    return;
            //}

            //var Takeresult = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //{
            //    PhotoSize = PhotoSize.Medium,
            //    SaveToAlbum = false,
            //});

            //if (Takeresult != null)
            //{
            //    imageView.Source = Takeresult.Path;
            //    imageView.IsEnabled = true;
            //    _data = Takeresult;
            //}
        //}

        //async Task Handle_Clicked(object sender, EventArgs e)
        //{
        //    //Crashes.GenerateTestCrash();
        //    this.Navigation.PushAsync(new SecondPage() { Title = "dsagsagsa" });
        //}

        //async void Handle_Clicked_1(object sender, System.EventArgs e)
        //{
        //    using (var image = await Plugin.ImageEdit.CrossImageEdit.Current.CreateImageAsync(_data.GetStream()))
        //    {
        //        var data = image.Resize(150).ToJpeg(100);
        //        imageView.Source = ImageSource.FromStream(() => new MemoryStream(data));
        //    }
        //}

        private List<AssetMetaDataQuestion> Convert(Questions questions)
        {
            GetChecklistDetailResponse checklistDetail = new GetChecklistDetailResponse();
            //checklistDetail.categoryName = portingData.categoryName;
            //checklistDetail.categoryId = portingData.categoryId;
            //checklistDetail.checkListId = portingData.checkListId;
            //checklistDetail.subContractor = portingData.subContractor;
            //checklistDetail.location = portingData.location;
            //checklistDetail.inspectionStatus = portingData.inspectionStatus;

            checklistDetail.questions = new List<AssetMetaDataQuestion>();
            foreach (Answer answer in questions.answers)
            {
                AssetMetaDataQuestion question = new AssetMetaDataQuestion();
                question.questionId = answer.question_id;
                question.questionText = answer.question_text;
                question.response = answer.response;
                question.isRequired = answer.is_Required;
                question.responseType = answer.response_type;

                switch (question.responseType.ToLower())
                {
                    case "yes|no":
                        question.responseType = QuestionTypes.YES_NO;
                        break;
                    case "select":
                        question.responseType = QuestionTypes.DROPDOWN;
                        break;
                    case "signature":
                    case "number":
                    case "simple text":
                        question.responseType = QuestionTypes.SIMPLE_TEXT;
                        break;
                    case "date":
                        question.responseType = QuestionTypes.CALENDAR;
                        break;
                    default:
                        question.responseType = QuestionTypes.SIMPLE_TEXT;
                        break;
                }

                question.comments = new List<CommentModel>();

                if (answer.comments != null)
                    foreach (CommentModel comment in answer.comments)
                    {
                        CommentModel comments = new CommentModel();
                        comments.note = comment.note;
                        comments.img = comment.img;
                        question.comments.Add(comments);
                    }

                question.optionList = new List<Option>();
                if (answer.question_options != null)
                    foreach (QuestionOption questionOption in answer.question_options)
                    {
                        Option option = new Option();
                        option.option = questionOption.result;
                        question.optionList.Add(option);
                    }
                checklistDetail.questions.Add(question);
            }
            return checklistDetail.questions;
        }
    }

    public class AssetMetaDataQuestion
    {
        [JsonIgnore]
        public string responseComment { get; set; }

        public int questionId { get; set; }

        public string questionText { get; set; }

        public string responseType { get; set; }

        public string editable { get; set; }

        public string response { get; set; }

        public bool isRequired { get; set; }

        public List<Option> optionList { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public byte[] imageBytes { get; set; }

        [JsonIgnore]
        public DateTime selectedDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                if (selectedDate != null && isDate)
                {
                    selectedDate = value;
                    //response = selectedDate.ToString(Constants.API_DATE_FORMAT_FOR_CHECKLIST_SUBMISSION);
                }
            }
        }

        [JsonIgnore]
        public List<Allcomments> commentsOrg { get; set; }

        [JsonIgnore]
        public int SelectedOptionIndex
        {
            get
            {
                if (responseType != null && responseType.Equals("Drop Down", StringComparison.InvariantCultureIgnoreCase) && optionList != null)
                {
                    return optionList.FindIndex(x => x.option.Equals(response, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (value >= 0 && value < optionList.Count)
                {
                    Option selectedOtion = optionList[value];
                    response = selectedOtion.option;
                }
            }
        }

        [JsonIgnore]
        public bool isEnabled { get; set; }

        public List<CommentModel> comments { get; set; }

        [JsonIgnore]
        private Option selectedOption;

        //public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public Option SelectedOption
        {
            get
            {
                return selectedOption;
            }
            set
            {
                if (selectedOption != null)
                {
                    selectedOption = value;
                    response = value.option;
                }
            }
        }

        [JsonIgnore]
        public bool isEntry
        {
            get
            {
                return responseType == null || responseType.Equals("simple text", StringComparison.InvariantCultureIgnoreCase) || responseType.Equals("ALPHANUMERIC", StringComparison.InvariantCultureIgnoreCase) || isEnabled == false ? true : false;
            }
            set
            {

            }
        }

        [JsonIgnore]
        public bool isNumber
        {
            get
            {
                return responseType != null && responseType.Equals("number", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
            }
            set
            {

            }
        }

        [JsonIgnore]
        public bool isPicker
        {
            get
            {
                return responseType != null && responseType.Equals("Drop Down", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
            }
            set
            {

            }
        }

        [JsonIgnore]
        public bool isSignature
        {
            get
            {
                return responseType != null && responseType.Equals("signature", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
            }
            set
            {

            }
        }

        //[JsonIgnore]
        //public bool isYesNo
        //{
        //    get
        //    {
        //        //if (responseType.Equals("yes|no") && null != response)
        //        //{
        //        //    yesSelected = response.ToLower() == "yes" ? true : false;
        //        //    noSelected = response.ToLower() == "no" ? true : false;
        //        //    naSelected = response.ToLower() == "n/a" ? true : false;
        //        //}

        //        return responseType != null && responseType.Contains("yes/no", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
        //    }
        //    set
        //    {

        //    }
        //}

        //[JsonIgnore]
        //public bool isOkNot
        //{
        //    get
        //    {
        //        //if (responseType.Equals("yes|no") && null != response)
        //        //{
        //        //    yesSelected = response.ToLower() == "yes" ? true : false;
        //        //    noSelected = response.ToLower() == "no" ? true : false;
        //        //    naSelected = response.ToLower() == "n/a" ? true : false;
        //        //}

        //        return responseType != null && responseType.Contains("ok/not ok", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
        //    }
        //    set
        //    {

        //    }
        //}

        //[JsonIgnore]
        //public bool isNa
        //{
        //    get
        //    {
        //        //if (responseType.Equals("yes|no") && null != response)
        //        //{
        //        //    yesSelected = response.ToLower() == "yes" ? true : false;
        //        //    noSelected = response.ToLower() == "no" ? true : false;
        //        //    naSelected = response.ToLower() == "n/a" ? true : false;
        //        //}

        //        return responseType != null && responseType.Contains("/na", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
        //    }
        //    set
        //    {

        //    }
        //}

        // [JsonIgnore]
        //public bool isYesNoNa
        //{
        //    get
        //    {
        //        //if (responseType.Equals("yes|no") && null != response)
        //        //{
        //        //    yesSelected = response.ToLower() == "yes" ? true : false;
        //        //    noSelected = response.ToLower() == "no" ? true : false;
        //        //    naSelected = response.ToLower() == "n/a" ? true : false;
        //        //}

        //        return responseType != null && responseType.Equals("yes/no/na", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
        //    }
        //    set
        //    {

        //    }
        //}

        [JsonIgnore]
        public bool isDate
        {
            get
            {
                return responseType != null && responseType.Equals("calendar", StringComparison.InvariantCultureIgnoreCase) && isEnabled == true ? true : false;
            }
            set
            {

            }
        }

        [JsonIgnore]
        public string CommentImgOrg
        {
            get; set;
        }

        //[JsonIgnore]
        //public List<Allcomments> comments
        //{
        //    get
        //    {
        //        return commentsOrg;
        //    }
        //    set
        //    {
        //        if (value != null && value.Count > 0 && (!string.IsNullOrEmpty(value[0].note) || !string.IsNullOrEmpty(value[0].img)))
        //        {
        //            //CommentImg = "comment.png";
        //        }
        //        else
        //        {
        //            //CommentImg = "uncomment.png";
        //            if (!string.IsNullOrEmpty(responseComment))
        //            {
        //                response = string.Empty;
        //            }
        //        }
        //        commentsOrg = value;
        //    }
        //}

        //[JsonIgnore]
        //public string CommentImg
        //{
        //    get
        //    {
        //        if (CommentImgOrg == null)
        //        {
        //            return "uncomment.png";
        //        }
        //        else
        //            return CommentImgOrg;
        //    }
        //    set
        //    {
        //        CommentImgOrg = value;
        //        //OnPropertyChanged("CommentImgOrg");
        //        OnPropertyChanged("CommentImg");
        //    }
    }

    public class QuestionTypes
    {
        public const string SIMPLE_TEXT = "simple text";
        public const string ALPHANUMERIC = "simple text";
        public const string DROPDOWN = "Drop Down";
        public const string CALENDAR = "calendar";
        public const string YES_NO = "yes/no";
        public const string OK_NOTOK = "ok/not ok";
        public const string YES_NO_NA = "yes/no/na";
        public const string OK_NOTOK_NA = "ok/not ok/na";
        public const string NUMBER = "number";
    }

    // Comment Model
    public class Allcomments
    {
        public string note { get; set; }
        public string img { get; set; }

        //public bool isRequired { get; set; }
    }

    public class Allcomments1
    {
        public string note { get; set; }
        public string img { get; set; }
        public string timeStamp { get; set; }
        //public bool isRequired { get; set; }
    }

    // Option Model
    public class Option
    {
        public string option { get; set; }
    }

    // Porting Data Model
    public class PortingData
    {
        //public int statusCode { get; set; }
        //public string statusMessage { get; set; }
        //public string categoryName { get; set; }
        //public int categoryId { get; set; }
        //public string qrBarCode { get; set; }
        //public string inspectedBy { get; set; }
        //public int nextLevelRoleId { get; set; }
        //public int nextLevelUserId { get; set; }
        //public int checkListId { get; set; }
        //public int inspectionId { get; set; }
        //public string inspectionStatus { get; set; }
        //public string subContractor { get; set; }
        //public string location { get; set; }
        public Questions questions { get; set; }
    }

    public class Questions
    {
        public List<Answer> answers { get; set; }
    }

    public class QuestionOption
    {
        public string id { get; set; }

        public string result { get; set; }
    }

    public class Answer
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public string response { get; set; }
        public string response_type { get; set; }
        //public int required { get; set; }
        //[JsonProperty]
        public List<CommentModel> comments { get; set; }
        //public object commentsOrg { get; set; }
        //public object options { get; set; }
        public List<QuestionOption> question_options { get; set; }
        //public bool radio_true { get; set; }
        //public bool is_Entry { get; set; }
        //public bool is_Number { get; set; }
        //public bool is_Picker { get; set; }
        //public bool is_Signature { get; set; }
        //public bool yesSelected { get; set; }
        //public bool noSelected { get; set; }
        //public bool is_Radio { get; set; }
        //public bool is_Date { get; set; }
        //public ResponseSelected response_selected { get; set; }
        //public string radio_response_selected { get; set; }
        //public string qid_response_type { get; set; }
        public bool is_Required { get; set; }
        //public DateTime StartDate { get; set; }
        //public object CommentImgOrg { get; set; }
        //public string CommentImg { get; set; }
        //public string dateresponse { get; set; }
    }

    public class CommentModel
    {
        //[JsonProperty]
        public string note { get; set; }

        //[JsonProperty]
        public string img { get; set; }
    }

    public class GetChecklistDetailResponse : ResponseBase
    {
        public string categoryName { get; set; }

        public int categoryId { get; set; }

        public string qrBarCode { get; set; }

        public string inspectedBy { get; set; }

        public int checkListId { get; set; }

        public string inspectionStatus { get; set; }

        public string subContractor { get; set; }

        public string location { get; set; }

        public int nextLevelUserId { get; set; }

        public int nextLevelRoleId { get; set; }

        public List<AssetMetaDataQuestion> questions { get; set; }

        //[JsonIgnore]
        //public List<User> nextLevelUsers { get; set; }
    }

    public class ResponseBase
    {
        //[JsonProperty("statusCode")]
        public int statusCode { get; set; }

        //[JsonProperty("statusMessage")]

        public string statusMessage { get; set; }
    }
}
