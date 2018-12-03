using PCLAppConfig;
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
        //---------------------------------------------------------------
        // Users controller end points
        public static string RegisterUserEndPoint {
            get {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["UsersAPIUrl"] + ConfigurationManager.AppSettings["RegisterUserAPI"];
                }
        }

        public static string LoginUserEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["UsersAPIUrl"] + ConfigurationManager.AppSettings["LoginUserAPI"];
            }
        }

        public static string GetUserByUsernameEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["UsersAPIUrl"] + ConfigurationManager.AppSettings["GetUserByUsernameAPI"];
            }
        }

        //---------------------------------------------------------------
        // Persons controller end points
        public static string AddNewPersonEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["PersonsAPIUrl"] + ConfigurationManager.AppSettings["AddNewPersonAPI"];
            }
        }

        public static string GetPersonEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["PersonsAPIUrl"] + ConfigurationManager.AppSettings["GetPersonAPI"];
            }
        }

        public static string GetPersonByCreatorEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["PersonsAPIUrl"] + ConfigurationManager.AppSettings["GetPersonByCreatorAPI"];
            }
        }

        public static string UpdatePersonEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["DataServiceAPIUrl"] + ConfigurationManager.AppSettings["PersonsAPIUrl"] + ConfigurationManager.AppSettings["UpdatePersonAPI"];
            }
        }

        //---------------------------------------------------------------
        // Faces controller end points
        public static string RecognizeFaceEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["RecognizerServiceAPIUrl"] + ConfigurationManager.AppSettings["FacesAPIUrl"] + ConfigurationManager.AppSettings["RecognizeFaceAPI"];
            }
        }

        public static string TrainFaceEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["RecognizerServiceAPIUrl"] + ConfigurationManager.AppSettings["FacesAPIUrl"] + ConfigurationManager.AppSettings["TrainFaceAPI"];
            }
        }

        public static string DetectFacesEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["RecognizerServiceAPIUrl"] + ConfigurationManager.AppSettings["FacesAPIUrl"] + ConfigurationManager.AppSettings["DetectFaceAPI"];
            }
        }
    }
}
