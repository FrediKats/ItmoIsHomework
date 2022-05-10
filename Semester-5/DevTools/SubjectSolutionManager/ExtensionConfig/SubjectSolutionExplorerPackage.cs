using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using SubjectSolutionManager.Tools;
using Task = System.Threading.Tasks.Task;

namespace SubjectSolutionManager.ExtensionConfig
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(SubjectSolutionExplorer))]
    [Guid(SubjectSolutionExplorerPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class SubjectSolutionExplorerPackage : AsyncPackage
    {
        public const string PackageGuidString = "e517804d-8cfb-47a6-850a-be2519cdee0e";

        public SubjectSolutionExplorerPackage()
        {
        }

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await SubjectSolutionExplorerCommand.InitializeAsync(this);

            IVsSolution pSolution = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            Configuration.SolutionManager = pSolution;
        }

        #endregion
    }
}
