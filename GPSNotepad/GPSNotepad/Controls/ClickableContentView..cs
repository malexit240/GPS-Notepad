using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GPSNotepad.Controls
{
    public class ClickableContentView : ContentView
    {
        public event EventHandler<Point> OnTouchesBegan;
        public event EventHandler<Point> OnTouchesCancelled;
        public event EventHandler<Point> OnTouchesEnded;
        public event EventHandler<Point> OnTouchesMoved;
        public event EventHandler OnInvalidate;

        private State _currentState = State.Default;
        protected State CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    UpdateState();
                }
            }
        }

        public ClickableContentView()
        {

            IsAnimated = true;
        }

        #region -- Public properties --

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ClickableContentView), default(ICommand));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
                    BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ClickableContentView), default(object));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty SelectedScaleProperty =
                    BindableProperty.Create(nameof(SelectedScale), typeof(double), typeof(ClickableContentView), 0.97);

        public double SelectedScale
        {
            get { return (double)GetValue(SelectedScaleProperty); }
            set { SetValue(SelectedScaleProperty, value); }
        }

        public static readonly BindableProperty IsAnimatedProperty =
            BindableProperty.Create(nameof(IsAnimated), typeof(bool), typeof(ClickableContentView), default(bool));

        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        public static readonly BindableProperty UseNativeGestureProperty = BindableProperty.Create(
            propertyName: nameof(UseNativeGesture),
            returnType: typeof(bool),
            declaringType: typeof(ClickableContentView),
            defaultValue: true);

        public bool UseNativeGesture
        {
            get => (bool)GetValue(UseNativeGestureProperty);
            set => SetValue(UseNativeGestureProperty, value);
        }

        protected virtual void UpdateState()
        {
            if (Math.Abs(SelectedScale - 1.0f) > 0.001 && IsAnimated)
            {
                uint animationMiliseconds = 100;
                if (CurrentState == State.Selected)
                {
                    this.ScaleTo(SelectedScale, animationMiliseconds);
                }
                else
                {
                    if (Math.Abs(Scale - 1.0f) > 0.001)
                    {
                        this.ScaleTo(1.0f);
                    }
                }
            }
        }

        #endregion

        #region -- Overrides --

        public void Invalidate()
        {
            OnInvalidate?.Invoke(this, EventArgs.Empty);
        }

        public bool TouchesBegan(System.Collections.Generic.IEnumerable<Point> points)
        {
            if (CurrentState != State.Disabled)
            {
                CurrentState = State.Selected;
            }

            OnTouchesBegan?.Invoke(this, points.First());
            return true;
        }

        public bool TouchesCancelled(System.Collections.Generic.IEnumerable<Point> points)
        {
            if (CurrentState != State.Disabled)
            {
                CurrentState = State.Default;
            }

            OnTouchesCancelled?.Invoke(this, points.First());
            return true;
        }

        public bool TouchesEnded(System.Collections.Generic.IEnumerable<Point> points)
        {
            if (CurrentState != State.Disabled)
            {
                var isTouchEndedInside = false;

                foreach (var item in points)
                {
                    if ((this.Bounds.Width >= item.X && item.X >= 0) &&
                        (this.Bounds.Height >= item.Y && item.Y >= 0))
                    {
                        isTouchEndedInside = true;
                    }
                }

                if (isTouchEndedInside)
                {
                    OnClicked();
                }

                CurrentState = State.Default;
            }

            OnTouchesEnded?.Invoke(this, points.First());
            return true;
        }

        public virtual bool TouchesMoved(System.Collections.Generic.IEnumerable<Point> points)
        {
            OnTouchesMoved?.Invoke(this, points.First());
            return true;
        }

        #endregion

        #region -- Private helpers --

        private void OnClicked()
        {
            if (IsEnabled && (Command?.CanExecute(CommandParameter) ?? false))
            {
                Command?.Execute(CommandParameter);
            }
        }

        #endregion

        protected enum State
        {
            Default,
            Selected,
            Disabled
        }
    }
}
