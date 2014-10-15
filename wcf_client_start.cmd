SET CurrentDir=%~dp0
SET BinDir=Vdk.AutoCompleter.Wcf.Client\bin\Release\
SET Host=localhost
SET Port=813

CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.Wcf.Client.exe -h %Host% -p %Port%