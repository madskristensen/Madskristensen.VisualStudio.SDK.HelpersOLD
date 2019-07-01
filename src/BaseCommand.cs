using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace VS
{
    public abstract class BaseCommand<T> where T : BaseCommand<T>, new()
    {
        protected BaseCommand(Guid guid, int id)
        {
            Guid = guid;
            Id = id;
        }

        public Guid Guid { get; private set; }
        public int Id { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            var instance = new T();
            var cmdId = new CommandID(instance.Guid, instance.Id);

            var menuCmd = new OleMenuCommand((s, e) => { instance.Execute(package, (OleMenuCommand)s, (OleMenuCmdEventArgs)e); }, cmdId);
            menuCmd.BeforeQueryStatus += (s, e) => { instance.BeforeQueryStatus(package, (OleMenuCommand)s, (OleMenuCmdEventArgs)e); };
            menuCmd.Supported = false;

            if (await package.GetServiceAsync(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
            {
                commandService.AddCommand(menuCmd);
            }
        }

        protected virtual Task ExecuteAsync(AsyncPackage package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {
            return Task.CompletedTask;
        }

        protected virtual void Execute(Package package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async delegate
            {
                try
                {
                    await ExecuteAsync((AsyncPackage)package, cmd, e);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });

        }

        protected virtual void BeforeQueryStatus(Package package, OleMenuCommand cmd, OleMenuCmdEventArgs e)
        {

        }
    }
}
