using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace YAWL.Common.ViewHelpers
{
    public class OnEnter
    {
        abstract class BaseOnEnterHandler
        {
            public Page Root { get; set; }

            public Control Source { get; set; }

            public BaseOnEnterHandler(Control source)
            {
                Source = source;
                Source.KeyDown += Source_KeyDown;
            }

            void Source_KeyDown(object sender, KeyRoutedEventArgs e)
            {
                if (e.Key == VirtualKey.Enter)
                {
                    OnEnter();
                }
            }

            protected abstract void OnEnter();

            public void Detach()
            {
                Source.KeyDown -= Source_KeyDown;
            }
        }

        class FocusOnEnterHandler : BaseOnEnterHandler
        {
            public Control Target { get; set; }

            public FocusOnEnterHandler(Control source, Control target)
                : base(source)
            {
                Target = target;
            }

            protected override void OnEnter()
            {
                if (Target != null) Target.Focus(FocusState.Programmatic);
            }
        }

        class InvokeCommandOnEnterHandler : BaseOnEnterHandler
        {
            public ICommand Command { get; set; }

            public InvokeCommandOnEnterHandler(Control source, ICommand command)
                : base(source)
            {
                Command = command;
            }

            protected override async void OnEnter()
            {
                if (Root != null)
                    Root.Focus(FocusState.Programmatic);

                await Task.Yield();

                if (Command != null)
                    Command.Execute(null);
            }
        }

        class UnFocusOnEnterHandler : BaseOnEnterHandler
        {
            public UnFocusOnEnterHandler(Control control)
                : base(control)
            {
            }

            protected override void OnEnter()
            {
                if (Source != null)
                {
                    Source.IsEnabled = false;
                    Source.IsEnabled = true;
                }
            }
        }

        static readonly Dictionary<Page, List<BaseOnEnterHandler>> Bindings = new Dictionary<Page, List<BaseOnEnterHandler>>();

        #region Focus attached property
        public static readonly DependencyProperty FocusProperty = DependencyProperty.RegisterAttached(
            "Focus", typeof(string), typeof(OnEnter), new PropertyMetadata(default(string), FocusPropertyChangedCallback));

        private static void FocusPropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var source = o as Control;
            if (source == null)
                return;

            RoutedEventHandler handler = null;
            handler = (sender, args) =>
            {
                source.Loaded -= handler;

                var page = source.GetParentByType<Page>();
                if (page == null)
                    return;

                var target = page
                    .GetLogicalChildrenBreadthFirst()
                    .OfType<Control>()
                    .FirstOrDefault(fe => fe.Name == (string)e.NewValue);

                if (target == null)
                    return;

                if (!Bindings.ContainsKey(page))
                    Bindings.Add(page, new List<BaseOnEnterHandler>());

                Bindings[page].Add(new FocusOnEnterHandler(source, target));
                page.Unloaded += page_Unloaded;
            };
            source.Loaded += handler;
        }

        static void page_Unloaded(object sender, RoutedEventArgs e)
        {
            var page = sender as Page;
            if (page == null)
                return;

            page.Unloaded -= page_Unloaded;
            List<BaseOnEnterHandler> bindings;
            if (Bindings.TryGetValue(page, out bindings))
            {
                foreach (var binding in bindings)
                {
                    binding.Detach();
                }

                bindings.Clear();
            }
        }

        public static void SetFocus(DependencyObject element, string value)
        {
            element.SetValue(FocusProperty, value);
        }

        public static string GetFocus(DependencyObject element)
        {
            return (string)element.GetValue(FocusProperty);
        }
        #endregion

        #region InvokeCommand attached property

        public static readonly DependencyProperty InvokeCommandProperty = DependencyProperty.RegisterAttached(
            "InvokeCommand", typeof(ICommand), typeof(OnEnter), new PropertyMetadata(default(ICommand), InvokeCommandChangedCallback));

        private static void InvokeCommandChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var source = o as Control;
            if (source == null)
                return;

            RoutedEventHandler handler = null;
            handler = (sender, args) =>
            {
                source.Loaded -= handler;

                var page = source.GetParentByType<Page>();
                if (page == null)
                    return;

                var target = e.NewValue as ICommand;
                if (target == null)
                    return;

                if (!Bindings.ContainsKey(page))
                    Bindings.Add(page, new List<BaseOnEnterHandler>());

                Bindings[page].Add(new InvokeCommandOnEnterHandler(source, target)
                {
                    Root = page
                });
                page.Unloaded += page_Unloaded;
            };
            source.Loaded += handler;
        }

        public static void SetInvokeCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(InvokeCommandProperty, value);
        }

        public static ICommand GetInvokeCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(InvokeCommandProperty);
        }
        #endregion

        #region UnFocus attached property
        public static readonly DependencyProperty UnFocusProperty = DependencyProperty.RegisterAttached(
            "UnFocus", typeof(bool), typeof(OnEnter), new PropertyMetadata(default(bool), UnFocusChangedCallback));

        private static void UnFocusChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var source = o as Control;
            if (source == null)
                return;

            RoutedEventHandler handler = null;
            handler = (sender, args) =>
            {
                source.Loaded -= handler;

                var page = source.GetParentByType<Page>();
                if (page == null)
                    return;

                if (!Bindings.ContainsKey(page))
                    Bindings.Add(page, new List<BaseOnEnterHandler>());

                Bindings[page].Add(new UnFocusOnEnterHandler(source)
                {
                    Root = page
                });
                page.Unloaded += page_Unloaded;
            };
            source.Loaded += handler;
        }

        public static void SetUnFocus(DependencyObject element, bool value)
        {
            element.SetValue(UnFocusProperty, value);
        }

        public static bool GetUnFocus(DependencyObject element)
        {
            return (bool)element.GetValue(UnFocusProperty);
        }
        #endregion
    }
}
