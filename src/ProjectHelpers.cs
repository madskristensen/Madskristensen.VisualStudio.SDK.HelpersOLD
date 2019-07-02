using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace VS
{
    public static class ProjectHelpers
    {
        public static async Task<Project> GetSelectedProjectAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            return await GetSelectedItemAsync() as Project;
        }

        public static async Task<ProjectItem> GetSelectedProjectItemAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            return await GetSelectedItemAsync() as ProjectItem;
        }

        private static async Task<object> GetSelectedItemAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            IVsMonitorSelection vsMonitorSelection = await ServiceProvider.GetGlobalServiceAsync<SVsShellMonitorSelection, IVsMonitorSelection>();

            vsMonitorSelection.GetCurrentSelection(out IntPtr ppHier, out uint pitemid, out _, out IntPtr ppSC);

            try
            {
                if (ppHier == IntPtr.Zero)
                {
                    return null;
                }

                // multiple items are selected.
                if (pitemid == (uint)VSConstants.VSITEMID.Selection)
                {
                    return null;
                }


                if (Marshal.GetTypedObjectForIUnknown(ppHier, typeof(IVsHierarchy)) is IVsHierarchy hierarchy)
                {
                    ErrorHandler.ThrowOnFailure(hierarchy.GetProperty(pitemid, (int)__VSHPROPID.VSHPROPID_ExtObject, out object item));
                    return item;
                }
            }
            catch (Exception ex)
            {
                VsShellUtilities.LogError(ex.Source, ex.ToString());
            }
            finally
            {
                if (ppHier != IntPtr.Zero)
                {
                    Marshal.Release(ppHier);
                }

                if (ppSC != IntPtr.Zero)
                {
                    Marshal.Release(ppSC);
                }
            }

            return null;
        }
    }
}
