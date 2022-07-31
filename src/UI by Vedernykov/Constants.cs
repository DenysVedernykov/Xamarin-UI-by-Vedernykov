namespace UI_by_Vedernykov
{
    public static class Constants
    {
        public static class API
        {
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
