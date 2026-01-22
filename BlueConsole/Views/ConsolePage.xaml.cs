using BlueConsole.Commands;
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

public class CommandTemplateSelector : DataTemplateSelector
{
    public DataTemplate? SimpleTemplate { get; set; }
    public DataTemplate? InputTemplate { get; set; }
    public DataTemplate? EnumTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            SimpleCommand => SimpleTemplate,
            InputCommand => InputTemplate,
            EnumCommand => EnumTemplate,
            _ => null
        };
    }
}