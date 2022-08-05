using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class MarqueeBehavior : Behavior<VisualElement>
    {
        private bool _isStartTimer;

        private double _parentWidth;
        private double _visualElementWidth;

        private VisualElement _visualElement;
        private VisualElement _visualElementsParent;

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

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Renderer":
                    _isStartTimer = !_isStartTimer;

                    if (_isStartTimer)
                    {
                        StartAnimation();
                    }

                    break;

                case "Width":
                    _visualElementWidth = _visualElement.Width;

                    StartAnimation();
                    break;

                case "Parent":

                    if (sender is VisualElement visualElement)
                    {
                        _visualElementsParent = (VisualElement)visualElement.Parent;
                        _visualElementsParent.PropertyChanged += OnPropertyChangedParent;
                    }

                    break;
            }
        }

        private void OnPropertyChangedParent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Width")
            {
                _parentWidth = _visualElementsParent.Width;

                StartAnimation();
            }
        }

        private void StartAnimation()
        {
            if (_visualElementWidth > 0 && _parentWidth > 0)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
                {
                    if (Math.Abs(_visualElement.TranslationX) > _visualElementWidth)
                    {
                        _visualElement.TranslationX = _parentWidth;
                    }

                    _visualElement.TranslationX -= 5f;

                    return _isStartTimer;
                });
            }

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
