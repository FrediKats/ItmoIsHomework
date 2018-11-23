using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using SubjectSolutionManager.Views;

namespace SubjectSolutionManager.ExtensionConfig
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
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
