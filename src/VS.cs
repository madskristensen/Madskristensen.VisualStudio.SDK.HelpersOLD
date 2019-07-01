using Madskristensen.VisualStudio.SDK.Helpers.Services;
using System;

namespace Madskristensen.VisualStudio.SDK.Helpers
{
    public class VS
    {
        public static Version Version { get; }

        public static StatusBar StatusBar => new StatusBar();
    }
}
