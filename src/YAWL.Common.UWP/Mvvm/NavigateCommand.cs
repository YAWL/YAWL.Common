using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;

namespace YAWL.Common.Mvvm
{
    public class NavigateCommand<TPage> : RelayCommand
        where TPage : Page
    {
        public NavigateCommand()
            : base(Execute)
        {

        }

        private static void Execute()
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(typeof(TPage));
            }
        }
    }

    public class NavigateCommand<TPage, TParameter> : RelayCommand<TParameter>
        where TPage : Page
    {
        public NavigateCommand()
            : base(Execute)
        {

        }

        private static void Execute(TParameter parameter)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(typeof(TPage), parameter);
            }
        }
    }
}
