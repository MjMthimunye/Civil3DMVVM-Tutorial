using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using Jacobian.Civil3D.Tools.Civil3DMVVM;
using Jacobian.Civil3D.Utilities;
using System.IO;
using System.Reflection;

[assembly: ExtensionApplication(typeof(Jacobian.Civil3D.Application))]

namespace Jacobian.Civil3D
{
    public class Application : IExtensionApplication
    {
        private static readonly string _addinDirectory =
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;
            ComponentManager.ItemInitialized += ComponentManager_ItemInitialized;
        }

        public void Terminate()
        {
            ComponentManager.ItemInitialized -= ComponentManager_ItemInitialized;
        }

        private void ComponentManager_ItemInitialized(object sernder, EventArgs e)
        {
            // Unsubscribe immediately — prevents duplicate tabs on re-load
            ComponentManager.ItemInitialized -= ComponentManager_ItemInitialized;

            RibbonControl ribbonControl = ComponentManager.Ribbon;

            // Tab 
            RibbonTab ribbonTab = new RibbonTab
            {
                Title = "Jacobian Dev",
                Id = "{4508B39F-17DD-452F-8565-FF699F580258}"
            };
            ribbonControl.Tabs.Add(ribbonTab);

            // Panel 
            RibbonPanelSource panelSource = new RibbonPanelSource { Title = "Jacobian" };
            RibbonPanel ribbonPanel = new RibbonPanel { Source = panelSource };
            ribbonTab.Panels.Add(ribbonPanel);

            // Button
            RibbonUtils.CreateButton(
                panelSource, new Civil3DMVVMCommandHandler(),
                "Civil3D MVVM",
                "Civil 3D MVVM Tutorial",
                _addinDirectory);
        }

        /// <summary>
        /// Resolves assemblies not found by the default loader.
        /// Looks first in the addin directory, then recursively in sub-folders.
        /// </summary>
        private static Assembly AssemblyResolveHandler(object _, ResolveEventArgs args)
        {
            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);

            if (loadedAssembly != null)
                return loadedAssembly;

            try
            {
                var assemblyName = new AssemblyName(args.Name);
                var assemblyPath = System.IO.Path.Combine(_addinDirectory, $"{assemblyName.Name}.dll");

                if (System.IO.File.Exists(assemblyPath))
                    return Assembly.LoadFrom(assemblyPath);

                // Search sub-directories as a fallback
                var match = Directory.GetFiles(_addinDirectory, "*.dll", SearchOption.AllDirectories)
                    .FirstOrDefault(dll => Path.GetFileNameWithoutExtension(dll).Equals(assemblyName.Name, StringComparison.OrdinalIgnoreCase));

                return match != null ? Assembly.LoadFrom(match) : null;
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }
    }
}
