using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UI_by_Vedernykov.ENums;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using MenuItem = UI_by_Vedernykov.Helpers.MenuItem;

namespace UI_by_Vedernykov.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICommand _selectedMenuItemCommand;

        public MainPageViewModel(
            GradientViewViewModel gradientViewViewModel,
            VersionTrackingViewViewModel versionTrackingViewViewModel,
            LoginFormViewViewModel loginFormViewViewModel,
            ChartsAndGraphsViewViewModel chartsAndGraphsViewViewModel,
            FirebaseRealtimeDatabaseViewViewModel firebaseRealtimeDatabaseViewViewModel,
            INavigationService navigationService)
            : base(navigationService)
        {
            GradientViewViewModel = gradientViewViewModel;
            VersionTrackingViewViewModel = versionTrackingViewViewModel;
            LoginFormViewViewModel = loginFormViewViewModel;
            ChartsAndGraphsViewViewModel = chartsAndGraphsViewViewModel;
            FirebaseRealtimeDatabaseViewViewModel = firebaseRealtimeDatabaseViewViewModel;

            _selectedMenuItemCommand = new AsyncCommand(OnSelectedMenuItemCommand, allowsMultipleExecutions: false);
            _showAboutAppPageCommand = new AsyncCommand(OnShowAboutAppPageCommand, allowsMultipleExecutions: false);

            _menuItems = new()
            {
                new()
                {
                    State = EPages.Main,
                    Title = "Main",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.SetFocusOnEntryCompleted,
                    Title = "Set Focus On Entry",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.Gradients,
                    Title = "Gradients",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.VersionTracking,
                    Title = "Version Tracking",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.AutoScroll,
                    Title = "Auto Scroll",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.LoginForm,
                    Title = "Login Form",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.ChartsAndGraphs,
                    Title = "Charts & Graphs",
                    TapCommand = _selectedMenuItemCommand,
                },
                new()
                {
                    State = EPages.FirebaseRealtimeDatabase,
                    Title = "Firebase Realtime Database",
                    TapCommand = _selectedMenuItemCommand,
                },
            };

            SelectedMenuItem = _menuItems.FirstOrDefault();
            _stateSideMenu = SideMenuState.MainViewShown;
        }

        #region -- Public properties --

        public GradientViewViewModel GradientViewViewModel { get; set; }

        public VersionTrackingViewViewModel VersionTrackingViewViewModel { get; set; }

        public LoginFormViewViewModel LoginFormViewViewModel { get; set; }

        public ChartsAndGraphsViewViewModel ChartsAndGraphsViewViewModel { get; set; }

        public FirebaseRealtimeDatabaseViewViewModel FirebaseRealtimeDatabaseViewViewModel { get; set; }

        private MenuItem _selectedMenuItem;
        public MenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value);
        }

        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private SideMenuState _stateSideMenu;
        public SideMenuState StateSideMenu
        {
            get => _stateSideMenu;
            set => SetProperty(ref _stateSideMenu, value);
        }

        private ICommand _showAboutAppPageCommand;
        public ICommand ShowAboutAppPageCommand
        {
            get => _showAboutAppPageCommand;
            set => SetProperty(ref _showAboutAppPageCommand, value);
        }

        #endregion

        #region -- Private helpers --

        private Task OnSelectedMenuItemCommand()
        {
            StateSideMenu = SideMenuState.MainViewShown;

            return Task.CompletedTask;
        }

        private Task OnShowAboutAppPageCommand()
        {
            StateSideMenu = SideMenuState.RightMenuShown;

            return Task.CompletedTask;
        }

        #endregion
    }
}
