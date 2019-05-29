using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Controls;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace IVSCalc
{
    public sealed partial class InAppDoc : UserControl
    {
        static bool IsSet = false;
        RoutedEventHandler CloseFce;

        public string Version { get; set; }

        public InAppDoc()
        {
            InitializeComponent();

            var v = Package.Current.Id.Version;

            Version = $"Verze: {v.Major}.{v.Minor}.{v.Build}";

            KeyDown += InAppDoc_KeyDown;
        }

        private void InAppDoc_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Escape:
                    CloseHelp();
                    break;

                case Windows.System.VirtualKey.F1:
                    HelpButton_Click(null, null);
                    break;

                default:
                    break;
            }
        }

        public void SetClose(RoutedEventHandler CloseFceArg)
        {
            Close.Focus(FocusState.Keyboard);

            if (!IsSet)
            {
                IsSet = true;
                CloseFce += CloseFceArg;
                Close.Click += CloseFce;
            }
        }

        public void CloseHelp()
        {
            CloseFce?.DynamicInvoke(null, null);
        }

        private async void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var mpak = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var f = await mpak.GetFileAsync("Napoveda.pdf");
            var success = await Windows.System.Launcher.LaunchFileAsync(f);
        }
    }
}
