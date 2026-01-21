; -- sync.iss --

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!
#define SemanticVersion() \
   GetVersionComponents("..\tobii-interface\bin\x64\Release\net8.0-windows\tobii-interface.exe", Local[0], Local[1], Local[2], Local[3]), \
   Str(Local[0]) + "." + Str(Local[1]) + ((Local[2]>0) ? "." + Str(Local[2]) : "")
    

#define verStr_ StringChange(SemanticVersion(), '.', '-')

[Setup]
AppName=Tobii Interface
AppVerName=Tobii Interface V{#SemanticVersion()}
DefaultDirName={commonpf}\EPL\Tobii Interface\V{#SemanticVersion()}
OutputDir=Output
DefaultGroupName=EPL
AllowNoIcons=yes
OutputBaseFilename=Tobii_Interface_{#verStr_}
UsePreviousAppDir=no
UsePreviousGroup=no
UsePreviousSetupType=no
DisableProgramGroupPage=yes
PrivilegesRequired=admin

[Dirs]
Name: "{commonappdata}\EPL";

[Files]
Source: "..\tobii-interface\Images\Eye.ico"; DestDir: "{app}"; Flags: replacesameversion;
Source: "..\tobii-interface\bin\x64\Release\net8.0-windows\*.*"; DestDir: "{app}"; Flags: replacesameversion;
Source: "..\CHANGELOG.md"; DestDir: "{app}"; Flags: replacesameversion;

[Icons]
Name: "{commondesktop}\Tobii Interface"; Filename: "{app}\tobii-interface.exe"; IconFilename: "{app}\Eye.ico"; IconIndex: 0;


