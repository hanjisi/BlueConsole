using AndroidX.Lifecycle;
using BlueConsole.ViewModels;
using System.Collections.Specialized;

namespace BlueConsole.Views;

public partial class ConsolePage : ContentPage
{
    private ConsolePageViewModel vm;
    public ConsolePage(ConsolePageViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
        vm.Logs.CollectionChanged += LogsCollectionChanged;
    }


    private void LogsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LogList.ScrollTo(vm.Logs.Count-1, position: ScrollToPosition.End, animate: true);
            });
        }
        
    }
}