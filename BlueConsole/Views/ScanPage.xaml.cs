using BlueConsole.ViewModels;

namespace BlueConsole.Views;

public partial class ScanPage : ContentPage
{
	public ScanPage(ScanPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}