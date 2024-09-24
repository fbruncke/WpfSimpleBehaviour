using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace WpfSimpleBehaviour
{
    class MoveBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClicked;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClicked;
            base.OnDetaching();
        }

        void OnClicked(object sender, EventArgs args)
        {
            Button btnSource = sender as Button;
            if (btnSource == null) return;

            btnSource.RenderTransform = new RotateTransform();
            btnSource.RenderTransformOrigin = new Point(0.5,0.5);

            Storyboard storyboard = new Storyboard();
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(2.0)),
                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut }
                //EasingFunction = new BounceEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(rotateAnimation, btnSource);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
            storyboard.Children.Add(rotateAnimation);
            
            storyboard.Begin(btnSource);
        }
    }
}

