using System;
using UI_by_Vedernykov.Controls;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class ShakeInvalidValue : Behavior<CustomNoBorderEntry>
    {
        private CustomNoBorderEntry _entry = new();

        #region -- Public properties --

        public VisualElement? View { get; set; }

        public Color FocusedValidTextColor { get; set; }

        public Color FocusedInvalidTextColor { get; set; }

        public Color UnfocusedTextColor { get; set; }

        public Color FocusedValidBackgroundColor { get; set; }

        public Color FocusedInvalidBackgroundColor { get; set; }

        public Color UnfocusedBackgroundColor { get; set; }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(CustomNoBorderEntry bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable is CustomNoBorderEntry entry)
            {
                _entry = entry;
                _entry.IsValidChanged += HandlerPropertyChanged;
            }
        }

        protected override void OnDetachingFrom(CustomNoBorderEntry bindable)
        {
            _entry.PropertyChanged -= HandlerPropertyChanged;

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region-- Private helpers --

        private void HandlerPropertyChanged(object sender, EventArgs e)
        {
            var view = View is null
                ? _entry
                : View;

            if (!_entry.IsValid && _entry.ShouldResponseToInvalidValue)
            {
                _entry.TextColor = FocusedInvalidTextColor;
                view.BackgroundColor = FocusedInvalidBackgroundColor;

                view.RotateTo(7, 250, Easing.SpringOut).ContinueWith((x) =>
                {
                    view.RotateTo(1, 250, Easing.SpringOut).ContinueWith((x) =>
                    {
                        view.RotateTo(6, 200, Easing.SpringOut).ContinueWith((x) =>
                        {
                            view.RotateTo(0, 200, Easing.SpringOut).ContinueWith((x) =>
                            {
                                var isTextEmpty = string.IsNullOrEmpty(_entry.Text);

                                _entry.TextColor = _entry.IsFocused
                                    ? (_entry.IsValid || isTextEmpty) ? FocusedValidTextColor : FocusedInvalidTextColor
                                    : UnfocusedTextColor;

                                view.BackgroundColor = _entry.IsFocused
                                    ? (_entry.IsValid || isTextEmpty) ? FocusedValidBackgroundColor : FocusedInvalidBackgroundColor
                                    : UnfocusedBackgroundColor;
                            });
                        });
                    });
                });
            }
        }

        #endregion
    }
}
