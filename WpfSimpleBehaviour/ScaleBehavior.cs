using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WpfSimpleBehaviour
{
    class ScaleBehavior : Behavior<UIElement>
    {
        private int count = 0;
        protected override void OnAttached()
        {
            AssociatedObject.MouseEnter += OnClicked;
            
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= OnClicked;
            base.OnDetaching();
        }

        void OnClicked(object sender, EventArgs args)
        {
            UIElement source = sender as UIElement;
            if (source == null) return;

            count++;

            source.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform st = new ScaleTransform();

            if (count % 2 == 1)
            {
                st.ScaleX = -2;
                st.ScaleY = -2;
            }
            else {
                st.ScaleX = 1;
                st.ScaleY = 1;

            }
            source.RenderTransform = st;            

        }
    }


}
