' Developer Express Code Central Example:
' Binding Master and Detail focused rows to ViewModel objects
' 
' This example demonstrates how to use ViewModel properties to track and control
' focused row changes both for Master and Detail grids. This capability is
' achieved by creating attached behavior that handles all necessary events
' (especially for the focused view and row changing, both in the detail and master
' grids). The logic of focusing rows can be changed inside this behavior depending
' on your requirements. For example, you may want not to focus the first detail
' row when a master row is expanded. If so, change the MasterRowExpanded event
' handler as follows:
' 
' void MasterGridMasterRowExpanded(object sender,
' RowEventArgs e)
' {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
' GridControl).View.FocusedRow = null;
' }
' 
' 
' 
' 
' So, the main idea of the example is
' to show how the row focusing logic can be defined in the GridControl that
' operates in Master-Detail mode.
' 
' Important note:the approach shown in the
' example will work only with the DataControlDetailDescriptor, as other
' descriptors use custom templates to display detail content that is not
' synchronized with the master grid.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4402

' Developer Express Code Central Example:
' How to create a master-detail grid
' 
' In the 12.1 version we implemented a Master-Detail feature, and now we provide
' it out of the box. We have added a new solution, which shows how to use our new
' approach.
' 
' With later versions of our controls you can use the previous
' workaround:
' To accomplish this, it is necessary to use one GridControl as a
' template for a data row of another GridControl.
' Limitations of the previous
' approach:
' Vertical scrolling is performed per master row. For an example on how
' to implement vertical scrolling of details, please see 'Persistent Row State' in
' the DXGrid's demo.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E1000

Imports System.Reflection
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("Q184311")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("Q184311")>
<Assembly: AssemblyCopyright("Copyright ©  2008")>
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


<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)> 'where the generic resource dictionary is located - where theme specific resource dictionaries are located
    '(used if a resource is not found in the page, 
    ' or application resource dictionaries)
    '(used if a resource is not found in the page, 
    ' app, or any theme specific resource dictionaries)


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
