using UnrealBuildTool;
using System.IO;

public class MySQLConnectorUE4Plugin : ModuleRules
{
    public MySQLConnectorUE4Plugin(TargetInfo Target)
    {
        UEBuildConfiguration.bForceEnableExceptions = true;

        RulesAssembly r;
		FileReference CheckProjectFile;
		UProjectInfo.TryGetProjectForTarget("MyGame", out CheckProjectFile);
		
		r = RulesCompiler.CreateProjectRulesAssembly(CheckProjectFile);
		FileReference f = r.GetModuleFileName( this.GetType().Name );
		//File.WriteAllText("c:/temp/qqq2.txt", f.CanonicalName );
		
        string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "x64" : "x86";
        string ThirdPartyPath = Path.GetFullPath( Path.Combine(this.ModuleDirectory, "../../ThirdParty/" ) );
       
	    string LibrariesPath = Path.Combine(ThirdPartyPath, "MySQLConnector", "Lib");

        if (Target.Platform == UnrealTargetPlatform.Win64 || Target.Platform == UnrealTargetPlatform.Win32) {
            string PlatformString = (Target.Platform == UnrealTargetPlatform.Win64) ? "x64" : "x86";
            string LibraryName = Path.Combine(LibrariesPath, "mariadbclient." + PlatformString + ".lib");
            PublicAdditionalLibraries.Add(LibraryName);
        } else if (Target.Platform == UnrealTargetPlatform.Linux) {
            string LinuxPath = Path.Combine(ThirdPartyPath, "LinuxConnector", "lib");
            string LibraryName = Path.Combine(LinuxPath, "libmysqlclient.so");
            PublicAdditionalLibraries.Add(LibraryName);
        }

        PrivateIncludePaths.AddRange(new string[] { "MySQLConnectorUE4Plugin/Private" });
        PublicIncludePaths.AddRange(new string[] { "MySQLConnectorUE4Plugin/Public" });
		
		string IncludesPath = Path.Combine(ThirdPartyPath, "MySQLConnector", "Include");
        PublicIncludePaths.Add(IncludesPath);

		string IncludesPath2 = Path.Combine(ThirdPartyPath, "MySQLConnector", "Include", "cppconn");
        PublicIncludePaths.Add(IncludesPath2);

        PublicDependencyModuleNames.AddRange(new string[] { "Engine", "Core", "CoreUObject" });
    }
}