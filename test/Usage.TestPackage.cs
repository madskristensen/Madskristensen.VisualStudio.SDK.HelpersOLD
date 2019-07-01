using Madskristensen.VisualStudio.SDK.Helpers;
using Madskristensen.VisualStudio.SDK.Helpers.Services;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using static Microsoft.VisualStudio.VSConstants;
using Task = System.Threading.Tasks.Task;

namespace Usage.Test
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("2f7ada1b-2c5a-43b0-94bf-c965165aa69f")]
    [ProvideAutoLoad(UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class UsageTestPackage : AsyncPackage
    {

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            await VS.StatusBar.SetTextAsync("This is great");
            await VS.StatusBar.StartAnimationAsync(StatusAnimation.Find);
            await Task.Delay(2000);
            await VS.StatusBar.EndAnimationAsync(StatusAnimation.Find);
            await VS.StatusBar.SetTextAsync("ost");
            await Task.Delay(2000);
            await VS.StatusBar.ClearAsync();
        }
    }
}
