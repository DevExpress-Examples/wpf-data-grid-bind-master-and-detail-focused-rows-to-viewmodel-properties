' Developer Express Code Central Example:
' GridControl - How to bind Master and Detail focused rows to ViewModel objects to follow the MVVM pattern in your application
' 
' This example demonstrates how to use ViewModel properties to track and control
' focused row changes both for Master and Detail grids. This capability is
' achieved by creating attached behavior that handles all necessary events
' (especially for the focused view and row changing, both in the detail and master
' grids). The logic of focusing rows can be changed inside this behavior depending
' on your requirements. For example, you may want not to focus the first detail
' row when a master row is expanded. If so, change the MasterRowExpanded event
' handler as follows:
' void MasterGridMasterRowExpanded(object sender,
' RowEventArgs e)
' {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
' GridControl).View.FocusedRow = null;
' }
' So, the main idea of the example is to
' show how the row focusing logic can be defined in the GridControl that operates
' in Master-Detail mode.
' Note that the approach shown in the example will work
' only with the DataControlDetailDescriptor, as other descriptors use custom
' templates to display detail content that is not synchronized with the master
' grid.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4402



Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("E4402")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("E4402")>
<Assembly: AssemblyCopyright("Copyright ©  2012")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(False)>

'In order to begin building localizable applications, set 
'<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
'inside a <PropertyGroup>.  For example, if you are using US english
'in your source files, set the <UICulture> to en-US.  Then uncomment
'the NeutralResourceLanguage attribute below.  Update the "en-US" in
'the line below to match the UICulture setting in the project file.

'[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


	'(used if a resource is not found in the page, 
	' or application resource dictionaries)
	'(used if a resource is not found in the page, 
	' app, or any theme specific resource dictionaries)
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>


' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' [assembly: AssemblyVersion("1.0.*")]
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
