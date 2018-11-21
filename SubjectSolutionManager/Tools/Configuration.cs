using Microsoft.VisualStudio.Shell.Interop;

namespace SubjectSolutionManager.Tools
{
    public class Configuration
    {
        public static IVsSolution SolutionManager { get; set; }
    }
}