using Madskristensen.VisualStudio.SDK.Helpers;
using Microsoft.VisualStudio.Shell;
using System;
using Task = System.Threading.Tasks.Task;

namespace UsageTest
{
    public class MyCommand : BaseCommand<MyCommand>
    {
        public MyCommand() : base(Guid.NewGuid(), 124)
        { }

        protected override async Task ExecuteAsync(AsyncPackage package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {
            await VS.StatusBar.SetTextAsync("This is great");
            await Task.Delay(2000);
            await VS.StatusBar.StartAnimationAsync(StatusAnimation.Find);
            await Task.Delay(2000);
            await VS.StatusBar.EndAnimationAsync(StatusAnimation.Find);
            await Task.Delay(2000);
            await VS.StatusBar.SetTextAsync("ost");
            await Task.Delay(2000);
            await VS.StatusBar.ClearAsync();
        }
    }
}
