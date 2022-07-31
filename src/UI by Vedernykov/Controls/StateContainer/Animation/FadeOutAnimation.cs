using Xamarin.Forms;

namespace UI_by_Vedernykov.Controls.StateContainer.Animation
{
    public class FadeOutAnimation : AnimationBase
    {
        #region -- Overrides --

        public override void Apply(View view)
        {
            if (view != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    view.FadeTo(0, 1);
                });
            }
        }

        #endregion
    }
}