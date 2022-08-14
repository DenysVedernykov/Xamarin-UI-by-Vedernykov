using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace UI_by_Vedernykov.ViewModels
{
    public class LoginFormViewViewModel : BaseViewModel
    {
        public LoginFormViewViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        private bool _isRadialGradient;
        public bool IsRadialGradient
        {
            get => _isRadialGradient;
            set => SetProperty(ref _isRadialGradient, value);
        }

        private ICommand? _turnCommand;
        public ICommand TurnCommnad => _turnCommand ?? new AsyncCommand(OnTurnCommandAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Private helpers --

        private Task OnTurnCommandAsync()
        {
            IsRadialGradient = !IsRadialGradient;

            return Task.CompletedTask;
        }

        #endregion
    }
}
