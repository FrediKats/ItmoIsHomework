using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using SubjectSolutionManager.Views;

namespace SubjectSolutionManager.ExtensionConfig
{
    [Guid("a6ef7515-6689-46fd-bf7e-b92b9724a4bd")]
    public class SubjectSolutionExplorer : ToolWindowPane
    {
        public SubjectSolutionExplorer() : base(null)
        {
            this.Caption = "SubjectSolutionExplorer";

            this.Content = new SubjectSolutionExplorerControl();
        }
    }
}
