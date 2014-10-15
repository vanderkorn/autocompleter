SET CurrentDir=%~dp0
SET BinDir=Vdk.AutoCompleter.Thrift.Client\bin\Release\
SET Host=localhost
SET Port=823
CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.Thrift.Client.exe -h %Host% -p %Port%