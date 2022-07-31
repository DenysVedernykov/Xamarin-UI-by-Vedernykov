using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using UI_by_Vedernykov.ViewModels;
using UI_by_Vedernykov.ViewModels.Mobile;
using UI_by_Vedernykov.Views;
using Xamarin.Forms;

namespace UI_by_Vedernykov
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        #region -- Public properties --

        public static bool IsTablet = Device.Idiom == TargetIdiom.Tablet;

        #endregion

        #region -- Overrides --

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Dialogs
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            //ViewModels
            containerRegistry.RegisterSingleton<GradientViewViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            App.Current.UserAppTheme = OSAppTheme.Dark;

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region -- Public static helpers --

        public static T Resolve<T>() => Current.Container.Resolve<T>();

        #endregion
    }
}
