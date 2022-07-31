using System;
using UI_by_Vedernykov.Controls;
using Xamarin.CommunityToolkit.Behaviors.Internals;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class SetFocusOnEntryNextOrPreviousBehavior : BaseBehavior<VisualElement>
    {
        private CustomNoBorderEntry _entry = new();

        private bool _isOldValid = false;
        private bool _isNewValid = false;

        #region -- Public properties --

        public bool IsInvalidValueMoveToPreviousElement { get; set; }

        public bool IsInvalidValueMoveToNextElement { get; set; } = true;

        public VisualElement? NextElement { get; set; }

        public VisualElement? PreviousElement { get; set; }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable is CustomNoBorderEntry entry)
            {
                _entry = entry;
                _entry.IsValidChanged += HandlerIsValidChanged;
                _entry.Completed += HandlerCompleted;
                _entry.TextChanged += HandlerTextChanged;
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            _entry.IsValidChanged -= HandlerIsValidChanged;
            _entry.Completed -= HandlerCompleted;
            _entry.TextChanged -= HandlerTextChanged;

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region -- Private helpers --

        private void HandlerTextChanged(object sender, TextChangedEventArgs e)
        {
            if ((IsInvalidValueMoveToPreviousElement || _isOldValid) && string.IsNullOrEmpty(_entry.Text) && PreviousElement is not null)
            {
                ChangeFocus(PreviousElement);
            }
            else if (_entry.Text?.Length == _entry.MaxLength && (IsInvalidValueMoveToNextElement || _entry.IsValid))
            {
                ChangeFocus(NextElement);
            }
        }

        private void HandlerCompleted(object sender, EventArgs e)
        {
            if (_entry.IsValid)
            {
                ChangeFocus(NextElement);
            }
            else
            {
                _entry.ShouldResponseToInvalidValue = true;
                _entry.IsValid = false;
            }
        }

        private void HandlerIsValidChanged(object sender, EventArgs e)
        {
            _isOldValid = _isNewValid;
            _isNewValid = _entry.IsValid;
        }

        private void ChangeFocus(VisualElement? element)
        {
            if (element is null)
            {
                _entry.Unfocus();
            }
            else
            {
                element.Focus();
            }
        }

        #endregion
    }
}
