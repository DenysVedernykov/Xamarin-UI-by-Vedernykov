using System;
using System.ComponentModel;
using UI_by_Vedernykov.ENums;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class MarqueeBehavior : Behavior<VisualElement>
    {
        private bool _canStartTimer;
        private bool _isValidSizeView;

        private int _repeatCount;

        private double _parentWidth;
        private double _visualElementWidth;

        private double _parentHeight;
        private double _visualElementHeight;

        private VisualElement _visualElement;
        private VisualElement _visualElementsParent;

        #region -- Public properties --

        public static readonly BindableProperty SecondsToScrollProperty = BindableProperty.Create(
            propertyName: nameof(SecondsToScroll),
            returnType: typeof(double),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: 5d,
            defaultBindingMode: BindingMode.OneWay);

        public double SecondsToScroll
        {
            get => (double)GetValue(SecondsToScrollProperty);
            set => SetValue(SecondsToScrollProperty, value);
        }

        public static readonly BindableProperty RepeatCountProperty = BindableProperty.Create(
            propertyName: nameof(RepeatCount),
            returnType: typeof(int),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: 0,
            defaultBindingMode: BindingMode.OneWay);

        public int RepeatCount
        {
            get => (int)GetValue(RepeatCountProperty);
            set => SetValue(RepeatCountProperty, value);
        }

        public static readonly BindableProperty EasingProperty = BindableProperty.Create(
            propertyName: nameof(Easing),
            returnType: typeof(Easing),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: Easing.Linear,
            defaultBindingMode: BindingMode.OneWay);

        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public static readonly BindableProperty NeedScrollToStartIfAnimationFinishProperty = BindableProperty.Create(
            propertyName: nameof(NeedScrollToStartIfAnimationFinish),
            returnType: typeof(bool),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: true,
            defaultBindingMode: BindingMode.OneWay);

        public bool NeedScrollToStartIfAnimationFinish
        {
            get => (bool)GetValue(NeedScrollToStartIfAnimationFinishProperty);
            set => SetValue(NeedScrollToStartIfAnimationFinishProperty, value);
        }

        public static readonly BindableProperty DirectionProperty = BindableProperty.Create(
            propertyName: nameof(Direction),
            returnType: typeof(EDirectionMove),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: EDirectionMove.Bounce,
            defaultBindingMode: BindingMode.OneWay);

        public EDirectionMove Direction
        {
            get => (EDirectionMove)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            propertyName: nameof(Orientation),
            returnType: typeof(StackOrientation),
            declaringType: typeof(MarqueeBehavior),
            defaultValue: StackOrientation.Vertical,
            defaultBindingMode: BindingMode.OneWay);

        public StackOrientation Orientation
        {
            get => (StackOrientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
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
                    _canStartTimer = !_canStartTimer;

                    StartAnimationIfNeed();

                    break;

                case nameof(_visualElement.Width):
                    _visualElementWidth = _visualElement.Width;

                    _isValidSizeView = IsValidSizeView();

                    break;

                case nameof(_visualElement.Height):
                    _visualElementHeight = _visualElement.Height;

                    _isValidSizeView = IsValidSizeView();

                    break;

                case nameof(_visualElement.Parent):

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
            if (e.PropertyName == nameof(_visualElementsParent.Width))
            {
                _parentWidth = _visualElementsParent.Width;

                _isValidSizeView = IsValidSizeView();
            }
            else if (e.PropertyName == nameof(_visualElementsParent.Height))
            {
                _parentHeight = _visualElementsParent.Height;

                _isValidSizeView = IsValidSizeView();
            }
        }

        private bool CanRepeat()
        {
            return RepeatCount == 0 || RepeatCount > _repeatCount;
        }

        private bool IsValidSizeView()
        {
            return _visualElementHeight > 0 && _parentHeight > 0 && _visualElementWidth > 0 && _parentWidth > 0;
        }

        private void StartAnimationIfNeed()
        {
            if (_canStartTimer && CanRepeat())
            {
                StartAnimation();
            }
        }

        private void TranslateTo(double x, double y, double finishPositionX, double finishPositionY)
        {
            var milliseconds = (uint)TimeSpan.FromSeconds(SecondsToScroll).TotalMilliseconds;

            _visualElement.TranslateTo(x, y, milliseconds, Easing).ContinueWith((x) =>
            {
                _visualElement.TranslationX = finishPositionX;
                _visualElement.TranslationY = finishPositionY;

                if (RepeatCount > _repeatCount)
                {
                    _repeatCount++;
                }

                if (NeedScrollToStartIfAnimationFinish && !CanRepeat())
                {
                    _visualElement.TranslateTo(0, 0, 250, Easing);
                }

                StartAnimationIfNeed();
            });
        }

        private void StartAnimation()
        {
            if (CanRepeat())
            {
                if (_isValidSizeView)
                {
                    double x = 0;
                    double y = 0;

                    double finishPositionX = 0;
                    double finishPositionY = 0;

                    if (Direction == EDirectionMove.StartToEnd)
                    {
                        if (Orientation == StackOrientation.Vertical)
                        {
                            y = 0;
                            finishPositionY -= _visualElementHeight - _parentHeight;
                        }
                        else
                        {
                            x = 0;
                            finishPositionX -= _visualElementWidth - _parentWidth;
                        }
                    }
                    else if (Direction == EDirectionMove.EndToStart)
                    {
                        if (Orientation == StackOrientation.Vertical)
                        {
                            y -= _visualElementHeight - _parentHeight;
                            finishPositionY = _parentHeight;
                        }
                        else
                        {
                            x -= _visualElementWidth;
                            finishPositionX = _parentWidth;
                        }
                    }
                    else
                    {
                        if (Orientation == StackOrientation.Vertical)
                        {
                            y -= _visualElementHeight;
                            finishPositionY = _parentHeight;
                        }
                        else
                        {
                            x -= _visualElementWidth;
                            finishPositionX = _parentWidth;
                        }
                    }

                    TranslateTo(x, y, finishPositionX, finishPositionY);
                }
                else
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                    {
                        StartAnimationIfNeed();

                        return false;
                    });
                }
            }
        }

        #endregion
    }
}
