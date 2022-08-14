using System;
using System.ComponentModel;
using UI_by_Vedernykov.ENums;
using Xamarin.Forms;

namespace UI_by_Vedernykov.Behaviors
{
    public class MarqueeBehavior : Behavior<VisualElement>
    {
        private double _x = 0;
        private double _y = 0;

        private double _finishPositionX = 0;
        private double _finishPositionY = 0;

        private bool _canStartTimer;
        private bool _isValidSizeView;

        private int _repeatCount;

        private double _parentWidth;
        private double _visualElementWidth;
        private double _visualElementWidthRequest;

        private double _parentHeight;
        private double _visualElementHeight;
        private double _visualElementHeightRequest;

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
                    _visualElementWidthRequest = _visualElement.WidthRequest;

                    _isValidSizeView = IsValidSizeView();

                    break;

                case nameof(_visualElement.Height):
                    _visualElementHeight = _visualElement.Height;
                    _visualElementHeightRequest = _visualElement.HeightRequest;

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
            return _visualElementHeight > 0
                && _parentHeight > 0
                && _visualElementWidth > 0
                && _parentWidth > 0;
        }

        private void StartAnimationIfNeed()
        {
            if (_canStartTimer && CanRepeat())
            {
                StartAnimation();
            }
        }

        private void TryTranslateTo(double x, double y, double finishPositionX, double finishPositionY)
        {
            try
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
            catch (Exception)
            {
            }
        }

        private void StartAnimation()
        {
            if (CanRepeat())
            {
                if (_isValidSizeView)
                {
                    var isOrientationVertical = Orientation == StackOrientation.Vertical;

                    var isHeightVisualElementGreater = _visualElementHeight > _parentHeight;
                    var isWidthVisualElementGreater = _visualElementWidth > _parentWidth;

                    var offsetX = _parentWidth - _visualElementWidth;
                    var offsetY = _parentHeight - _visualElementHeight;

                    var widthRequest = _visualElementWidthRequest == -1
                        ? _visualElementWidth
                        : _visualElementWidthRequest;

                    var heightRequest = _visualElementHeightRequest == -1
                        ? _visualElementHeight
                        : _visualElementHeightRequest;

                    var offsetXRequest = _parentWidth - widthRequest;
                    var offsetYRequest = _parentHeight - heightRequest;

                    var offsetYIf = isHeightVisualElementGreater
                        ? offsetY
                        : offsetYRequest;

                    var offsetXIf = isWidthVisualElementGreater
                        ? offsetX
                        : offsetXRequest;

                    switch (Direction)
                    {
                        case EDirectionMove.StartToEnd:
                            if (isOrientationVertical)
                            {
                                if (isHeightVisualElementGreater)
                                {
                                    _y = 0;
                                    _finishPositionY = offsetY;
                                }
                                else
                                {
                                    _finishPositionY = 0;
                                    _y = offsetYRequest;
                                }
                            }
                            else
                            {
                                if (isWidthVisualElementGreater)
                                {
                                    _x = 0;
                                    _finishPositionX = offsetX;
                                }
                                else
                                {
                                    _finishPositionX = 0;
                                    _x = offsetXRequest;
                                }
                            }

                            break;
                        case EDirectionMove.EndToStart:
                            if (isOrientationVertical)
                            {
                                if (isHeightVisualElementGreater)
                                {
                                    _finishPositionY = 0;
                                    _y = offsetYIf;
                                }
                                else
                                {
                                    _y = 0;
                                    _finishPositionY = offsetYIf;
                                }
                            }
                            else
                            {
                                if (isWidthVisualElementGreater)
                                {
                                    _finishPositionX = 0;
                                    _x = offsetXIf;
                                }
                                else
                                {
                                    _x = 0;
                                    _finishPositionX = offsetXIf;
                                }
                            }

                            break;
                        case EDirectionMove.CircleStartToEnd:
                            if (isOrientationVertical)
                            {
                                _y = _parentHeight;
                                _finishPositionY = isHeightVisualElementGreater
                                    ? -_visualElementHeight
                                    : -heightRequest;
                            }
                            else
                            {
                                _x = _parentWidth;
                                _finishPositionX = isWidthVisualElementGreater
                                    ? -_visualElementWidth
                                    : -widthRequest;
                            }

                            break;
                        case EDirectionMove.CircleEndToStart:
                            if (isOrientationVertical)
                            {
                                _finishPositionY = _parentHeight;
                                _y = isHeightVisualElementGreater
                                    ? -_visualElementHeight
                                    : -heightRequest;
                            }
                            else
                            {
                                _finishPositionX = _parentWidth;
                                _x = isWidthVisualElementGreater
                                    ? -_visualElementWidth
                                    : -widthRequest;
                            }

                            break;
                        default:
                            if (isOrientationVertical)
                            {
                                _y = _finishPositionY = _y == 0
                                    ? offsetYIf
                                    : 0;
                            }
                            else
                            {
                                _x = _finishPositionX = _x == 0
                                    ? offsetXIf
                                    : 0;
                            }

                            break;
                    }

                    TryTranslateTo(_x, _y, _finishPositionX, _finishPositionY);
                }
                else
                {
                    try
                    {
                        Device.StartTimer(TimeSpan.FromMilliseconds(350), () =>
                        {
                            StartAnimationIfNeed();

                            return false;
                        });
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
