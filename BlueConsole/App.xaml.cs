using BlueConsole.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BlueConsole
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Routing.RegisterRoute("ConPage", typeof(ConPage));
            return new Window(new AppShell());
        }
    }
}