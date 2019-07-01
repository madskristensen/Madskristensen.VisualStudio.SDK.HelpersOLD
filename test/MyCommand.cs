using Microsoft.VisualStudio.Shell;
using System;
using VS;
using Task = System.Threading.Tasks.Task;

namespace UsageTest
{
    public class MyCommand : BaseCommand<MyCommand>
    {
        public MyCommand() : base(Guid.NewGuid(), 124)
        { }

        protected override async Task ExecuteAsync(AsyncPackage package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {
            await StatusBar.SetTextAsync("This is great");
            await Task.Delay(2000);
            await StatusBar.StartAnimationAsync(StatusAnimation.Find);
            await Task.Delay(2000);
            await StatusBar.EndAnimationAsync(StatusAnimation.Find);
            await Task.Delay(2000);
            await StatusBar.SetTextAsync("ost");
            await Task.Delay(2000);
            await StatusBar.ClearAsync();
        }
    }
}
