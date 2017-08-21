// <copyright file="AssemblyInfo.cs" company="Kolky">
//  __  __         __ __
// |  |/  |.-----.|  |  |--.--.--.
// |     ( |  _  ||  |    (|  |  |
// |__|\__||_____||__|__|__|___  |
//                         |_____|
//
// Copyright (c) Alexander van der Kolk 2017. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.md file for full license information.
// </copyright>

using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Allium")]
[assembly: AssemblyDescription("Google Analytics v3 Tracker by Alexander van der Kolk")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Allium")]
[assembly: AssemblyCopyright("Copyright © Alexander van der Kolk 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e2d5f38e-a68a-41ee-aaf1-aaa1f61a327b")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0.0")]

// Expose to our test project and test dll's (see https://stackoverflow.com/q/6855648/253612)
[assembly: InternalsVisibleTo("Allium.Tests, PublicKey="
    + "00240000048000009400000006020000002400005253413100040000010001009935818938f9b6"
    + "e4b3e9a7060eb73074260094c9850df3a4acb06bef2f2aabfd5d58fb08f359d569f3bc20fffc4c"
    + "a5d27613b07f7816f8e773b1e17947fe20ba9d955fd83ba144104e92d03607ebf1efcc56a31ce2"
    + "d7c4230f8bc9df188b15791a74c6a60617afa4b09af58b5f1ba59d4d5a5f18d590c96e62fe97e0"
    + "d7acd0f0")]

[assembly: InternalsVisibleTo("Rhino.Mocks, PublicKey="
    + "00240000048000009400000006020000002400005253413100040000010001009D1CF4B75B7218"
    + "B141AC64C15450141B1E5F41F6A302AC717AB9761FA6AE2C3EE0C354C22D0A60AC59DE41FA285D"
    + "572E7CF33C320AA7FF877E2B7DA1792FCC6AA4EB0B4D8294A2F74CB14D03FB9B091F751D6DC49E"
    + "626D74601692C99EAB7718ED76A40C36D39AF842BE378B677E6E4EAE973F643D7065241AD86ECC"
    + "156D81AB")]

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey="
    + "0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99"
    + "c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654"
    + "753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46"
    + "ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484c"
    + "f7045cc7")]

// Set resource language
[assembly: NeutralResourcesLanguage("en")]