SET CurrentDir=%~dp0
SET BinDir=wcfclient\
SET Host=localhost
SET Port=813

CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.Wcf.Client.exe -h %Host% -p %Port%