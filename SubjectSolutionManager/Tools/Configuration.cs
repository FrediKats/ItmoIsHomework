using Microsoft.VisualStudio.Shell.Interop;

namespace SubjectSolutionManager.Tools
{
    public static class Configuration
    {
        public static IVsSolution SolutionManager { get; set; }
    }
}