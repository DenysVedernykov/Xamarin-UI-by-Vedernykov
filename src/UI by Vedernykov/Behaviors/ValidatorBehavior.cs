using System;
using System.Text.RegularExpressions;
using UI_by_Vedernykov.Controls;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class ValidatorBehavior : Behavior<CustomNoBorderEntry>
    {
        #region -- Public properties --

        public bool ShouldSetOldValidValue { get; set; }

        public string? Match { get; set; }

        public string? NotMatch { get; set; }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(CustomNoBorderEntry bindable)
        {
            bindable.TextChanged += OnTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(CustomNoBorderEntry bindable)
        {
            bindable.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region-- Private helpers --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is CustomNoBorderEntry entry)
            {
                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    if (entry.IsValid)
                    {
                        entry.ShouldResponseToInvalidValue = false;
                        entry.IsValid = false;
                    }
                }
                else
                {
                    try
                    {
                        entry.ShouldResponseToInvalidValue = true;

                        var isValid = true;

                        if (Match is not null)
                        {
                            isValid = Regex.IsMatch(e.NewTextValue, Match, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                        }

                        if (NotMatch is not null)
                        {
                            isValid = !Regex.IsMatch(e.NewTextValue, NotMatch, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                        }

                        entry.IsValid = isValid;

                        if (ShouldSetOldValidValue && !isValid)
                        {
                            entry.Text = e.OldTextValue;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        #endregion
    }
}
