using System;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class MoveBackgroundRadialGradientBehavior : Behavior<VisualElement>
    {
        private bool _canAnimation;

        private double _x = 0;
        private double _y = 0;

        private double _percentSpacingStep = 0;

        private VisualElement _element;

        private RadialGradientBrush _gradientBrush = new();

        #region -- Public properties --

        public GradientStopCollection GradientStops { get; set; } = new();

        public Point Center { get; set; } = new(0, 0);

        public double Milliseconds { get; set; } = 1500;

        public double Interval { get; set; } = 50;

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            _element = bindable;
            _element.PropertyChanged += Element_OnPropertyChanged;

            _x = Center.X;
            _y = Center.Y;

            InitPercentSpacingStep();

            _gradientBrush = new()
            {
                Center = Center,
                GradientStops = GradientStops,
            };
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            _element.PropertyChanged -= Element_OnPropertyChanged;

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region-- Private helpers --

        private void InitPercentSpacingStep()
        {
            var height = _element.HeightRequest;
            var width = _element.WidthRequest;

            var step = Milliseconds / Interval;

            var s = (height + width) * 2;

            var spacingStep = s / step;

            _percentSpacingStep = spacingStep / s;
        }

        private void Element_OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                _canAnimation = !_canAnimation;

                if (_canAnimation)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(Interval), OnTimerTick);
                }
            }
            else if (e.PropertyName is "HeightRequest" or "WidthRequest")
            {
                InitPercentSpacingStep();
            }
        }

        private bool OnTimerTick()
        {
            if (_x <= 0)
            {
                if (_y <= 0)
                {
                    _x += _percentSpacingStep;
                }
                else
                {
                    _y -= _percentSpacingStep;
                }
            }
            else if(_x >= 1)
            {
                if (_y >= 1)
                {
                    _x -= _percentSpacingStep;
                }
                else
                {
                    _y += _percentSpacingStep;
                }
            }
            else
            {
                if (_y <= 0)
                {
                    _x += _percentSpacingStep;
                }
                else
                {
                    _x -= _percentSpacingStep;
                }
            }

            _gradientBrush.Center = new(_x, _y);
            _element.Background = _gradientBrush;

            return _canAnimation;
        }

        #endregion
    }
}
