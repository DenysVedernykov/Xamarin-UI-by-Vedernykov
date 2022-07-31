﻿using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace UI_by_Vedernykov.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<iOS>().SetUseSafeArea(true);

            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }

        #region -- Overrides --

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #endregion
    }
}
