using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using System.Threading.Tasks;
using UI_by_Vedernykov.Interfaces;
using Xamarin.Essentials;

namespace UI_by_Vedernykov.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, IInitializeAsync, INavigationAware, IDestructible, IPageActionsHandler
    {
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region -- Public properties --

        private IPopupNavigation _popupNavigation;
        public IPopupNavigation PopupNavigation => _popupNavigation ??= App.Resolve<IPopupNavigation>();

        #endregion

        #region -- Protected properties --

        protected INavigationService _navigationService { get; }

        protected bool IsInternetConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        #endregion

        #region -- IInitialize implementation --

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IInitializeAsync implementation --

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region -- INavigationAware implementation --

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion

        #region -- IDestructible implementation --

        public virtual void Destroy()
        {
        }

        #endregion

        #region -- IPageActionsHandler implementation --

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        #endregion
    }
}
