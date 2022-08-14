using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Controls
{
    public class CustomNoBorderEntry : Entry
    {
        #region -- Public properties --

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            propertyName: nameof(IsValid),
            returnType: typeof(bool),
            declaringType: typeof(CustomNoBorderEntry));

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set
            {
                SetValue(IsValidProperty, value);
                IsValidChanged?.Invoke(this, new EventArgs());
            }
        }

        public static readonly BindableProperty ShouldSetCursorPositionToEndProperty = BindableProperty.Create(
            propertyName: nameof(ShouldSetCursorPositionToEnd),
            returnType: typeof(bool),
            declaringType: typeof(CustomNoBorderEntry));

        public bool ShouldSetCursorPositionToEnd
        {
            get => (bool)GetValue(ShouldSetCursorPositionToEndProperty);
            set => SetValue(ShouldSetCursorPositionToEndProperty, value);
        }

        public static readonly BindableProperty ShouldSetFocusToNextElementProperty = BindableProperty.Create(
            propertyName: nameof(ShouldSetFocusToNextElement),
            returnType: typeof(bool),
            defaultValue: true,
            declaringType: typeof(CustomNoBorderEntry));

        public bool ShouldSetFocusToNextElement
        {
            get => (bool)GetValue(ShouldSetFocusToNextElementProperty);
            set => SetValue(ShouldSetFocusToNextElementProperty, value);
        }

        public static readonly BindableProperty ShouldResponseToInvalidValueProperty = BindableProperty.Create(
            propertyName: nameof(ShouldResponseToInvalidValue),
            returnType: typeof(bool),
            defaultValue: true,
            declaringType: typeof(CustomNoBorderEntry));

        public bool ShouldResponseToInvalidValue
        {
            get => (bool)GetValue(ShouldResponseToInvalidValueProperty);
            set => SetValue(ShouldResponseToInvalidValueProperty, value);
        }

        public event EventHandler<EventArgs> IsValidChanged;

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (ShouldSetCursorPositionToEnd && propertyName is nameof(IsFocused) or nameof(Text) && IsFocused)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CursorPosition = string.IsNullOrEmpty(Text)
                        ? 0
                        : Text.Length;
                });
            }
        }

        #endregion
    }
}
