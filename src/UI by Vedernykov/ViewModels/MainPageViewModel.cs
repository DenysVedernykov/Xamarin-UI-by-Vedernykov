using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UI_by_Vedernykov.ENums;
using UI_by_Vedernykov.ViewModels.Mobile;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using MenuItem = UI_by_Vedernykov.Helpers.MenuItem;

namespace UI_by_Vedernykov.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(
            GradientViewViewModel gradientViewViewModel,
            INavigationService navigationService)
            : base(navigationService)
        {
            GradientViewViewModel = gradientViewViewModel;

            _menuItems = new()
            {
                new()
                {
                    State = EPages.Main,
                    Title = "Main",
                },
                new()
                {
                    State = EPages.SetFocusOnEntryCompleted,
                    Title = "Set Focus On Entry",
                },
                new()
                {
                    State = EPages.Gradient,
                    Title = "Gradients",
                },
            };

            SelectedMenuItem = _menuItems.FirstOrDefault();
            _stateSideMenu = SideMenuState.MainViewShown;

            _changeSelectedMenuItemCommand = new AsyncCommand(OnChangeSelectedMenuItemCommand, allowsMultipleExecutions: false);
            _showAboutAppPageCommand = new AsyncCommand(OnShowAboutAppPageCommand, allowsMultipleExecutions: false);
        }

        #region -- Public properties --

        public GradientViewViewModel GradientViewViewModel { get; set; }

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

        private ICommand _changeSelectedMenuItemCommand;
        public ICommand ChangeSelectedMenuItemCommand
        {
            get => _changeSelectedMenuItemCommand;
            set => SetProperty(ref _changeSelectedMenuItemCommand, value);
        }

        private ICommand _showAboutAppPageCommand;
        public ICommand ShowAboutAppPageCommand
        {
            get => _showAboutAppPageCommand;
            set => SetProperty(ref _showAboutAppPageCommand, value);
        }

        #endregion

        #region -- Overrides --

        #endregion

        #region -- Private helpers --

        private Task OnChangeSelectedMenuItemCommand()
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
