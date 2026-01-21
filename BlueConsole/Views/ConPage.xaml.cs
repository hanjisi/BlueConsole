using System.Collections.ObjectModel;
using System.Text;

namespace BlueConsole.Views;

public partial class ConPage : ContentPage
{
    const double PanelHeight = 280;

    public ObservableCollection<LogItem> Logs { get; } = new();

    bool _panelVisible;
    bool _showHex;

    Timer _logTimer;
    int _counter;

    public ConPage()
    {
        InitializeComponent();
        BindingContext = this;

        ToolbarItems.Add(new ToolbarItem
        {
            Text = "HEX",
            Command = new Command(ToggleHex)
        });

        // 工具栏：操作面板
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "操作",
            Command = new Command(ToggleActionPanel)
        });

        // 工具栏：HEX / 文本切换
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "HEX",
            Command = new Command(ToggleHex)
        });

        StartMockLogs();
    }

    #region 模拟日志

    void StartMockLogs()
    {
        _logTimer = new Timer(AppendMockLog,null,1000,1000);
    }

    void AppendMockLog(object? state)
    {
        // 模拟一条“串口数据”
        var raw = CreateMockPacket();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            var item = new LogItem
            {
                Time = DateTime.Now,
                Raw = raw
            };

            item.UpdateDisplay(_showHex);
            Logs.Add(item);

            // 自动滚动到最新
            LogList.ScrollTo(item, position: ScrollToPosition.End, animate: false);
        });
    }

    byte[] CreateMockPacket()
    {
        _counter++;

        var text =
            $"PKT {_counter:D4}\r\n" +
            $"TEMP={(20 + _counter % 5)}.3\r\n" +
            $"SAL={(30 + _counter % 3)}.1\r\n" +
            $"END\r\n";

        return Encoding.ASCII.GetBytes(text);
    }

    #endregion

    #region HEX / 文本切换

    void ToggleHex()
    {
        _showHex = !_showHex;

        foreach (var log in Logs)
            log.UpdateDisplay(_showHex);
    }

    #endregion

    #region 操作面板（之前的逻辑，保留）

    void ToggleActionPanel()
    {
        if (_panelVisible)
            HidePanel();
        else
            ShowPanel();
    }

    async void ShowPanel()
    {
        _panelVisible = true;
        ActionPanel.IsVisible = true;
        ActionPanel.HeightRequest = 0;

        await AnimateHeight(ActionPanel, 0, PanelHeight, 200);
    }

    async void HidePanel()
    {
        _panelVisible = false;

        await AnimateHeight(ActionPanel, PanelHeight, 0, 180);
        ActionPanel.IsVisible = false;
    }

    Task AnimateHeight(VisualElement view, double from, double to, uint duration)
    {
        var tcs = new TaskCompletionSource();

        new Animation(
            v => view.HeightRequest = v,
            from,
            to)
            .Commit(
                view,
                "HeightAnim",
                16,
                duration,
                Easing.CubicOut,
                (v, c) => tcs.SetResult());

        return tcs.Task;
    }

    #endregion
}