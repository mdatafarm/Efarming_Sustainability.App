using Efarming_Sustainability.App.Views;
using Efarming_Sustainability.Core.Models;

namespace Efarming_Sustainability.App
{
    public partial class AppShell : Shell
    {
        private string _lastTab = "";
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(IndicatorsPage), typeof(IndicatorsPage));

            this.Navigated += OnShellNavigated;
        }


        private async void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            string current = Shell.Current?.CurrentItem?.CurrentItem?.Route;

            if (_lastTab == current)
            {
                // Usuario tocó el mismo tab mientras estaba en una subvista
                switch (current)
                {
                    case "IndicatorsPage":
                        await Shell.Current.GoToAsync("///IndicatorsPage");
                        break;

                    case "IndexFarms":
                        await Shell.Current.GoToAsync("///IndexFarms");
                        break;
                }
            }

            _lastTab = current;
        }


    }
}
