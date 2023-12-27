using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("PluginCRMPopup")]
[assembly: AssemblyDescription("Popup Screen for Avaya Workplace")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ITNavPro")]
[assembly: AssemblyProduct("PluginCRMPopup for Mimun Yashir")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("79c4306c-671b-437b-9add-ed58dffe7496")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
// 4.1 version each call pops up in the same ie tab
// 5.0 version each call opens new ie
// 6.0 added otp in uui and new url for unidentified client
// 7.0 otp=0/1 TZ URL, if ID# empty then UNIDENT URL
// 10.1 change UUI for Transfer
// 10.2 popup in the same tab for edge
// 10.3 Silently listening to the second popup has been cancelled and EMPTY UUI pops up also
// 10.4 bug for Silently listening
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


