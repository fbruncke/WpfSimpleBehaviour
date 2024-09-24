using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfSimpleBehaviour
{
    static class MoveBehaviorAttached
    {
        public static bool GetMove(DependencyObject obj)
        {
            return (bool)obj.GetValue(MoveProperty);
        }

        public static void SetMove(DependencyObject obj, bool value)
        {
            obj.SetValue(MoveProperty, value);
        }

        public static readonly DependencyProperty MoveProperty =
          DependencyProperty.RegisterAttached("Move",                       //move porperty defined
          typeof(bool), typeof(MoveBehaviorAttached),
          new PropertyMetadata(false, OnMoveOnlyChanged));

        private static void OnMoveOnlyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button btnSource = sender as Button;
            if (btnSource == null) return;

            var isActive = (bool)(e.NewValue);
            if( isActive  )
                btnSource.Click += OnClicked;
            else
                btnSource.Click -= OnClicked;

        }

        /// <summary>
        /// The dynamics behavior of the UI element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

        static void OnClicked(object sender, EventArgs args)
        {
            Button btnSource = sender as Button;
            if (btnSource == null) return;

            btnSource.RenderTransform = new RotateTransform();
            btnSource.RenderTransformOrigin = new Point(0.5, 0.5);

            Storyboard storyboard = new Storyboard();
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(2.0)),
                //EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut }
                EasingFunction = new BounceEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(rotateAnimation, btnSource);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            storyboard.Children.Add(rotateAnimation);

            storyboard.Begin(btnSource);
        }

    }
}
