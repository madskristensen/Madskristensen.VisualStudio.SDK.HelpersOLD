using Microsoft.VisualStudio.Shell;
using System;
using VS;
using Task = System.Threading.Tasks.Task;

namespace UsageTest
{
    public class MyCommand : BaseCommand<MyCommand>
    {
        public MyCommand() : base(new Guid("a19af14e-c6cf-4f7d-a704-75d3f8959afe"), 0x0100)
        { }

        protected override async Task ExecuteAsync(AsyncPackage package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {
            EnvDTE.ProjectItem item = await ProjectHelpers.GetSelectedProjectItemAsync();

            System.Windows.Forms.MessageBox.Show(item.ToString());

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
