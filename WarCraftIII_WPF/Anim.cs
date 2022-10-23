using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Windows.Media;
using WarCraftIII_Logic;

namespace WarCraftIII_WPF
{
    static class Anim
    {
        public static void AnimElementSize_MouseEnter(Button button)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = button.ActualWidth,
                To = button.ActualWidth * 0.95,
                Duration = TimeSpan.FromMilliseconds(50),

            };

            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = button.Height,
                To = button.ActualHeight * 0.95,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            button.BeginAnimation(Button.WidthProperty, widthAnimation);
            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        public static void AnimElementSize_MouseEnter(Image img)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = img.ActualWidth,
                To = img.ActualWidth * 0.95,
                Duration = TimeSpan.FromMilliseconds(50),

            };

            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = img.Height,
                To = img.ActualHeight * 0.95,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            img.BeginAnimation(Image.WidthProperty, widthAnimation);
            img.BeginAnimation(Image.HeightProperty, heightAnimation);
        }

        public static void AnimElementSize_MouseLeave(Button button)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = button.ActualWidth,
                To = button.ActualWidth * 1.05,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = button.Height,
                To = button.ActualHeight * 1.05,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            button.BeginAnimation(Button.WidthProperty, widthAnimation);
            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        public static void AnimElementSize_MouseLeave(Image img)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = img.ActualWidth,
                To = img.ActualWidth * 1.05,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = img.Height,
                To = img.ActualHeight * 1.05,
                Duration = TimeSpan.FromMilliseconds(50),
            };

            img.BeginAnimation(Image.WidthProperty, widthAnimation);
            img.BeginAnimation(Image.HeightProperty, heightAnimation);
        }

        public static async void AnimElementVisibility(UIElement element)
        {
            element.Opacity = 0;
            element.Visibility = Visibility.Visible;

            DoubleAnimation visAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(750),
            };

            DoubleAnimation hidAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(750),
            };

            element.BeginAnimation(UIElement.OpacityProperty, visAnimation);
            await Task.Delay(3000);
            element.BeginAnimation(UIElement.OpacityProperty, hidAnimation);
            await Task.Delay(1000);

            element.Visibility = Visibility.Hidden;
        }

        public static void AnimElementMove(Image element, Image img)
        {
            element.IsEnabled = false;
            TranslateTransform trans = new TranslateTransform();
            element.RenderTransform = trans;
            DoubleAnimation AnimationX = new DoubleAnimation
            {
                From = 0,
                To = -385,
                Duration = TimeSpan.FromMilliseconds(500),
            };


            trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);
            AnimElementMoveBack(element, img);
            
        }

        public static async void AnimElementMoveBack(Image element, Image img)
        {
            await Task.Delay(500);
            element.Opacity = 0;
            img.Visibility = Visibility.Visible;
            TranslateTransform trans = new TranslateTransform();
            element.RenderTransform = trans;
            DoubleAnimation AnimationX = new DoubleAnimation
            {
                From = -385,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1),
            };


            trans.BeginAnimation(TranslateTransform.XProperty, AnimationX);


            DoubleAnimation visAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(750),
            };

            element.BeginAnimation(UIElement.OpacityProperty, visAnimation);
            element.IsEnabled = true;
        }

        public static async void AnimFlashingLabel(Label label, Unit unit)
        {
            DoubleAnimation visAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(750),
            };

            DoubleAnimation hidAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(750),
            };

            if (unit.Inventory.Count >= 9)
            {
                for (int i = 0; i < 2; i++)
                {
                    label.BeginAnimation(UIElement.OpacityProperty, hidAnimation);
                    await Task.Delay(1000);
                    label.BeginAnimation(UIElement.OpacityProperty, visAnimation);
                    await Task.Delay(1000);
                }
            }
        }
    }
}
