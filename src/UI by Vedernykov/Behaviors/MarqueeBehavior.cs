using System;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class MarqueeBehavior : Behavior<VisualElement>
    {
        private bool _isStartTimer;

        private VisualElement _visualElement;

        #region -- Public properties --

        public static readonly BindableProperty PageWidthProperty = BindableProperty.Create(
            nameof(PageWidth),
            typeof(double),
            typeof(MarqueeBehavior));

        public double PageWidth
        {
            get => (double)GetValue(PageWidthProperty);
            set => SetValue(PageWidthProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(VisualElement element)
        {
            base.OnAttachedTo(element);

            _visualElement = element;

            element.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnDetachingFrom(VisualElement element)
        {
            element.PropertyChanged -= OnPropertyChanged;

            base.OnDetachingFrom(element);
        }

        #endregion

        #region -- Private helpers --

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _isStartTimer = !_isStartTimer;

                if (_isStartTimer)
                {
                    StartAnimation();
                }
            }
        }

        private void StartAnimation()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                var width = PageWidth == 0
                    ? _visualElement.Width
                    : PageWidth;

                if (Math.Abs(_visualElement.TranslationX) > width)
                {
                    _visualElement.TranslationX = width;
                }

                _visualElement.TranslationX -= 5f;

                return _isStartTimer;
            });
            //uint milliseconds = 50;

            //Device.StartTimer(TimeSpan.FromMilliseconds(milliseconds), () =>
            //{
            //    var width = PageWidth == 0
            //        ? _visualElement.Width
            //        : PageWidth;

            //    if (width > 0)
            //    {
            //        milliseconds = (uint)(50 * width / 5);
            //    }

            //    _visualElement.TranslateTo(-width, 0, milliseconds, Easing.SpringOut).ContinueWith((x) =>
            //    {
            //        _visualElement.TranslationX = width;
            //    });

            //    return _isStartTimer;
            //});
        }

        #endregion
    }
}
