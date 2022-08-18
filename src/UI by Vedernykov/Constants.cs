namespace UI_by_Vedernykov
{
    public static class Constants
    {
        public static class API
        {
            public const string FIREBASE_HOST_URL = "https://xamarin-ui-by-vedernykov-default-rtdb.europe-west1.firebasedatabase.app/";
            public const string FIREBASE_API_KEY = "AIzaSyCGEMRv72MCk3lytxLfeTvMYSK2PuwRb7s";
#if RELEASE
            public const string HOST_URL = "http://127.0.0.1";
#elif STAGE
            public const string HOST_URL = "http://127.0.0.1";
#elif DEV
            public const string HOST_URL = "http://127.0.0.1";
#else
            public const string HOST_URL = "http://127.0.0.1";
#endif
        }

        public static class Validators
        {
            public const string NUMBER = @"[0-9]";
        }
    }
}
