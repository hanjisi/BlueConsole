using BlueConsole.Views;

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
            
            Routing.RegisterRoute("ConsolePage", typeof(ConsolePage));
            return new Window(new AppShell());
        }
    }
}