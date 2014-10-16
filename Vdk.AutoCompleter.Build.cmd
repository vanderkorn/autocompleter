SET CurrentDir=%~dp0
SET MsbuildDir=%programfiles(x86)%\MSBuild\12.0\Bin
SET ReleaseDir=%CurrentDir%Vdk.AutoCompleter.Release
CD %CurrentDir%
"%MsbuildDir%\MSBuild.exe" Vdk.AutoCompleter.sln /t:Clean,Build /p:Configuration=Release

mkdir %ReleaseDir%

rd /s/q %ReleaseDir%\wcfserver
mkdir %ReleaseDir%\wcfserver
xcopy %CurrentDir%Vdk.AutoCompleter.Wcf.Server\bin\Release\* %ReleaseDir%\wcfserver  /s /e /c

rd /s/q %ReleaseDir%\wcfclient
mkdir %ReleaseDir%\wcfclient
xcopy %CurrentDir%Vdk.AutoCompleter.Wcf.Client\bin\Release\* %ReleaseDir%\wcfclient  /s /e /c

rd /s/q %ReleaseDir%\testclient
mkdir %ReleaseDir%\testclient
xcopy %CurrentDir%Vdk.AutoCompleter.TestClient\bin\Release\* %ReleaseDir%\testclient  /s /e /c

rd /s/q %ReleaseDir%\thriftserver
mkdir %ReleaseDir%\thriftserver
xcopy %CurrentDir%Vdk.AutoCompleter.Thrift.Server\bin\Release\* %ReleaseDir%\thriftserver  /s /e /c

rd /s/q %ReleaseDir%\thriftclient
mkdir %ReleaseDir%\thriftclient
xcopy %CurrentDir%Vdk.AutoCompleter.Thrift.Client\bin\Release\* %ReleaseDir%\thriftclient  /s /e /c