SET CurrentDir=%~dp0
SET BinDir=testclient\

CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.TestClient.exe
Pause