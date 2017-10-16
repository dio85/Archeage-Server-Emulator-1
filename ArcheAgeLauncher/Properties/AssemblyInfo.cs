using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

//Managing general information about the assembly is done using
//set of attributes.Change the values of these attributes to change the information,
//associated with the assembly.
[assembly: AssemblyTitle("ArcheAge Launcher")]
[assembly: AssemblyDescription("Launcher For ArcheAge Client")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ArcheAge Launcher")]
[assembly: AssemblyCopyright("Copyright © netcastiel 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//The ComVisible parameter with the FALSE value makes the types in the assembly invisible
//for COM components.If you want to access the type in this assembly via
//COM, set the ComVisible attribute to TRUE for this type.
[assembly: ComVisible(false)]

//To start building localized applications, specify
//<UICulture> CultureYouAreCodingWith</ UICulture> in the.csproj file
//within<PropertyGroup>.For example, if you are using English USA
//in its source files, set<UICulture> to en-US.Then undo the conversion to comment
//The NeutralResourceLanguage attribute is below.Update "en-US" in
//line at the bottom to ensure that the UICulture setting matches the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    //where there are resource dictionaries on specific topics
    //(used if the resource is not found on the page
    //or in the application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly
    //where is the dictionary of universal resources
    //(used if the resource is not found on the page,
    //in the application or in any resource dictionaries for a specific topic)
    )]


// The version information for the assembly consists of the following four values:
//
// Main version number
// Additional version number
// Build number
// Revision
//
// You can set all values or accept the build number and revision number by default,
// using "*", as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.*")]
