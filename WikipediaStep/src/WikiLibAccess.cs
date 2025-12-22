using System.Runtime.InteropServices;

namespace WikipediaStep;

public static class WikiLibAccess
{
    [DllImport("libWikiLib.dylib", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr ProcessPageUrl(string pageIn);
}