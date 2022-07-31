using Prism.Navigation;
using System.Collections.ObjectModel;
using UI_by_Vedernykov.Helpers;
using Xamarin.Forms;

namespace UI_by_Vedernykov.ViewModels.Mobile
{
    public class GradientViewViewModel : BaseViewModel
    {
        public GradientViewViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _pairsColors = new()
            {
                new()
                {
                    First = Color.FromHex("#FF2CDF"),
                    Second = Color.FromHex("#0014FF"),
                },
                new()
                {
                    First = Color.FromHex("#00FF5B"),
                    Second = Color.FromHex("#0014FF"),
                },
                new()
                {
                    First = Color.FromHex("#FFE53B"),
                    Second = Color.FromHex("#FF2525"),
                },
                new()
                {
                    First = Color.FromHex("#FFE53B"),
                    Second = Color.FromHex("#00FFFF"),
                },
                new()
                {
                    First = Color.FromHex("#00E1FD"),
                    Second = Color.FromHex("#FC007A"),
                },
                new()
                {
                    First = Color.FromHex("#00ESFF"),
                    Second = Color.FromHex("#1200FF"),
                },
                new()
                {
                    First = Color.FromHex("#FFES3B"),
                    Second = Color.FromHex("#FF005B"),
                },
                new()
                {
                    First = Color.FromHex("#FF0A6C"),
                    Second = Color.FromHex("#2D27FF"),
                },
            };
        }

        #region -- Public properties --

        private ObservableCollection<PairColors> _pairsColors;
        public ObservableCollection<PairColors> PairsColors
        {
            get => _pairsColors;
            set => SetProperty(ref _pairsColors, value);
        }

        #endregion
    }
}
