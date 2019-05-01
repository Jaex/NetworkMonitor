#define MyAppName "Network Monitor"
#define MyAppRootDirectory ".."
#define MyAppOutputDirectory MyAppRootDirectory + "\Output"
#define MyAppReleaseDirectory MyAppRootDirectory + "\NetworkMonitor\bin\Release"
#define MyAppFilename "NetworkMonitor.exe"
#define MyAppFilepath MyAppReleaseDirectory + "\" + MyAppFilename
#dim Version[4]
#expr ParseVersion(MyAppFilepath, Version[0], Version[1], Version[2], Version[3])
#define MyAppVersion Str(Version[0]) + "." + Str(Version[1]) + "." + Str(Version[2])
#define MyAppPublisher "Jaex"
#define MyAppId "832ed48d-596b-4f55-a1f0-d877e9c8a66c"

[Setup]
AppCopyright=Copyright (c) {#MyAppPublisher}
AppId={#MyAppId}
AppMutex={#MyAppId}
AppName={#MyAppName}
AppPublisher={#MyAppPublisher}
AppPublisherURL=https://github.com/Jaex/NetworkMonitor
AppSupportURL=https://github.com/Jaex/NetworkMonitor/issues
AppUpdatesURL=https://github.com/Jaex/NetworkMonitor/releases
AppVerName={#MyAppName} {#MyAppVersion}
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x86 x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DirExistsWarning=no
DisableStartupPrompt=yes
DisableWelcomePage=yes
DisableProgramGroupPage=yes
DisableReadyPage=no
DisableReadyMemo=no
DisableFinishedPage=no
LicenseFile={#MyAppRootDirectory}\LICENSE.txt
; .NET 4.6.2 is supported only on Windows 7 SP1 and up
MinVersion=0,6.1.7601
OutputBaseFilename=NetworkMonitor-{#MyAppVersion}-setup
OutputDir={#MyAppOutputDirectory}
PrivilegesRequired=none
ShowLanguageDialog=no
UninstallDisplayIcon={app}\{#MyAppFilename}
UninstallDisplayName={#MyAppName}
VersionInfoCompany={#MyAppPublisher}
VersionInfoTextVersion={#MyAppVersion}
VersionInfoVersion={#MyAppVersion}

#include "Scripts\lang\english.iss"

[Tasks]
Name: "CreateDesktopIcon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional shortcuts:"

[Files]
Source: "{#MyAppFilepath}"; DestDir: {app}; Flags: ignoreversion
Source: "{#MyAppRootDirectory}\LICENSE.txt"; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppFilename}"; WorkingDir: "{app}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"; WorkingDir: "{app}"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppFilename}"; WorkingDir: "{app}"; Tasks: CreateDesktopIcon; Check: not DesktopIconExists

[Run]
Filename: "{app}\{#MyAppFilename}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall

[CustomMessages]
DependenciesDir=Dependencies

#include "Scripts\products.iss"
#include "Scripts\products\stringversion.iss"
#include "Scripts\products\winversion.iss"
#include "Scripts\products\fileversion.iss"
#include "Scripts\products\dotnetfxversion.iss"
#include "scripts\products\dotnetfx46.iss"

[Code]
function InitializeSetup(): Boolean;
begin
  initwinversion();
  dotnetfx46(62);
  Result := true;
end;

function DesktopIconExists(): Boolean;
begin
  Result := FileExists(ExpandConstant('{userdesktop}\{#MyAppName}.lnk'));
end;