using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MT
{
    /// <summary>
    /// Separate this to NumericCellTextBox, and just NumericTextBox
    /// </summary>
    class NumericTextBoxBehavior : Behavior<TextBox>
    {
        //Think of moving this to class, at some point
        #region AttachedForStyle

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(NumericTextBoxBehavior),
                new UIPropertyMetadata(false, EnabledChanged));

        private static void EnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            if (element != null)
            {
                var behaviors = Interaction.GetBehaviors(element);
                var existing = behaviors.OfType<NumericTextBoxBehavior>().FirstOrDefault();
                bool newValue = (bool)e.NewValue;

                if (existing != null)
                {
                    if (newValue == false)
                    {
                        behaviors.Remove(existing);
                    }

                    //Can use some extra toughness code here
                }
                else
                {
                    if (newValue == true)
                    {
                        behaviors.Add(new NumericTextBoxBehavior());
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Indicates, whether system culture must be applied, if not, then the invariant culture is used
        /// </summary>
        public bool UseSystemCulture
        {
            get { return (bool)GetValue(UseSystemCultureProperty); }
            set { SetValue(UseSystemCultureProperty, value); }
        }

        /// <summary>
        /// Registering the dependecny property to control the behavior
        /// </summary>
        public static readonly DependencyProperty UseSystemCultureProperty =
            DependencyProperty.Register("UseSystemCulture", typeof(bool), typeof(NumericTextBoxBehavior), new PropertyMetadata(true));

        /// <summary>
        /// FIXME, I don't know how property changed works here, so we need some way to notify the regex, when the culture
        /// setting is changed, google it, make it clear, how those things work
        /// </summary>
        string DecimalSeparator
            => UseSystemCulture ? CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator : ".";

        /// <summary>
        /// In further use, we need to notify this somehow about UseSystemCultureChanging
        /// For know I do not know the way,, but this means I need to google dependency properties
        /// </summary>
        Regex NumericRegex { get; }

        public NumericTextBoxBehavior()
        {
            string separator = DecimalSeparator;
            if (separator == ".") separator = @"\" + separator;
            NumericRegex = new Regex($@"^\d*{separator}?\d*$");
        }

        bool IsTextValid(string text)
        {
            return NumericRegex.IsMatch(text);
        }

        protected override void OnAttached()
        {
            // Not paste safe
            base.OnAttached();
            AssociatedObject.PreviewTextInput += PreviewInputHandler;
            AssociatedObject.PreviewKeyDown += PreviewKeyHandler;
            
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= PreviewInputHandler;
            AssociatedObject.PreviewKeyDown -= PreviewKeyHandler;
            base.OnDetaching();
        }

        /// <summary>
        /// Skip on spacebars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyHandler(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        /// <summary>
        /// Skip on mismatching pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewInputHandler(object sender, TextCompositionEventArgs e)
        {
            TextBox box = sender as TextBox;

            if (box != null)
            {
                string resultText = box.Text;

                if (!(box.CaretIndex == box.Text.Length))
                {
                    if (box.SelectionLength > 0)
                    {
                        resultText = resultText.Remove(box.SelectionStart, box.SelectionLength);
                    }

                    resultText = resultText.Insert(box.CaretIndex, e.Text);
                }
                else
                {
                    resultText += e.Text;
                }

                e.Handled = !IsTextValid(resultText);
            }
        }
    }
}