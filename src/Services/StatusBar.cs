using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Madskristensen.VisualStudio.SDK.Helpers.Services
{
    public sealed class StatusBar
    {
        private async Task<IVsStatusbar> GetServiceAsync()
        {
            return await ServiceProvider.GetGlobalServiceAsync<SVsStatusbar, IVsStatusbar>();
        }

        public async Task<string> GetTextAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.GetText(out string pszText);
                return pszText;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task SetTextAsync(string text)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.SetText(text);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task ClearAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Clear();
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task StartAnimationAsync(StatusAnimation animation)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Animation(1, animation);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task EndAnimationAsync(StatusAnimation animation)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                IVsStatusbar statusBar = await GetServiceAsync();

                statusBar.FreezeOutput(0);
                statusBar.Animation(0, animation);
                statusBar.FreezeOutput(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
