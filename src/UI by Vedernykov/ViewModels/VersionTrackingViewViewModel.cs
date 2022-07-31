using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace UI_by_Vedernykov.ViewModels
{
    public class VersionTrackingViewViewModel : BaseViewModel
    {
        public VersionTrackingViewViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        #region -- Public properties --

        public bool IsFirstLaunchEver { get; set; } = VersionTracking.IsFirstLaunchEver;

        public bool IsFirstLaunchForCurrentVersion { get; set; } = VersionTracking.IsFirstLaunchForCurrentVersion;

        public bool IsFirstLaunchForCurrentBuild { get; set; } = VersionTracking.IsFirstLaunchForCurrentBuild;

        public string CurrentVersion { get; set; } = VersionTracking.CurrentVersion;

        public string CurrentBuild { get; set; } = VersionTracking.CurrentBuild;

        public string PreviousVersion { get; set; } = VersionTracking.PreviousVersion;

        public string PreviousBuild { get; set; } = VersionTracking.PreviousBuild;

        public string FirstInstalledVersion { get; set; } = VersionTracking.FirstInstalledVersion;

        public string FirstInstalledBuild { get; set; } = VersionTracking.FirstInstalledBuild;

        public List<string> VersionHistory { get; set; } = new(VersionTracking.VersionHistory);

        public List<string> BuildHistory { get; set; } = new(VersionTracking.BuildHistory);

        #endregion
    }
}
