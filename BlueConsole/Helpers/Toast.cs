namespace BlueConsole.Helpers
{
    public static class Toast
    {
        public async static void Show(string text, int seconds = 5)
        {
            var toast = CommunityToolkit.Maui.Alerts.Toast.Make(text);
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(seconds));
            await toast.Show(cts.Token);
        }
    }
}
