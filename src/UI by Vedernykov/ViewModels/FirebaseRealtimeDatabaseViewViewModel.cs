using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using User = UI_by_Vedernykov.Models.User;

namespace UI_by_Vedernykov.ViewModels
{
    public class FirebaseRealtimeDatabaseViewViewModel : BaseViewModel
    {
        private FirebaseClient _client;
        private FirebaseAuthLink _signIn;

        public FirebaseRealtimeDatabaseViewViewModel(INavigationService navigationService)
               : base(navigationService)
        {
            InitClient();
        }

        #region -- Public properties --

        private readonly ICommand? _addUserCommand;
        public ICommand AddUserCommnad => _addUserCommand ?? new AsyncCommand(OnAddUserCommnadAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Private helpers --

        private async Task InitClient()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.API.FIREBASE_API_KEY));

                _signIn = await authProvider.SignInWithEmailAndPasswordAsync("razom.info@gmail.com", "password");

                //var auth = await authProvider.CreateUserWithEmailAndPasswordAsync("razom.info@gmail.com", "password");
                _client = new FirebaseClient(
                    Constants.API.FIREBASE_HOST_URL,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(_signIn.FirebaseToken),
                    });
            }
            catch (Exception ex)
            {
            }
        }

        private async Task OnAddUserCommnadAsync()
        {
            try
            {
                var result = await _client
                    .Child($"users/{_signIn.User.LocalId}")
                    .PostAsync(new User()
                    {
                        Id = 1,
                        Name = "Denis",
                        Age = 28,
                    });
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine(ex.RequestUrl);
            }
        }

        #endregion
    }
}
