using System;
using System.Collections.Generic;
using System.Text;

namespace NightOwl.Xamarin
{
    public struct Config
    {

    }

    public struct APIEndPoints
    {
        public const string DataServiceAPIUrl = "https://nightowlwebservice.azurewebsites.net/api/";
        public const string RecognizerServiceAPIUrl = "https://nightowlpersonrecognitionservice.azurewebsites.net/api/";

        public const string UsersAPIUrl = "Users/";
        public const string PersonsAPIUrl = "Persons/";
        public const string FacesAPIUrl = "Faces/";

        public const string RegisterUserEndPoint = DataServiceAPIUrl + UsersAPIUrl + "Register/";
        public const string LoginUserEndPoint = DataServiceAPIUrl + UsersAPIUrl + "Login/";
        public const string GetUserByUsernameEndPoint = DataServiceAPIUrl + UsersAPIUrl + "GetUserByName/";

        public const string AddNewPersonEndPoint = DataServiceAPIUrl + PersonsAPIUrl + "Post/";
        public const string GetPersonEndPoint = DataServiceAPIUrl + PersonsAPIUrl + "Get/";
        public const string GetPersonByCreatorEndPoint = DataServiceAPIUrl + PersonsAPIUrl + "GetByCreatorId/";

        public const string RecognizeFaceEndPoint = RecognizerServiceAPIUrl + FacesAPIUrl + "Recognize/";
        public const string TrainFaceEndPoint = RecognizerServiceAPIUrl + FacesAPIUrl + "Train/";
        public const string DetectFacesEndPoint = RecognizerServiceAPIUrl + FacesAPIUrl + "Detect/";

    }
}
