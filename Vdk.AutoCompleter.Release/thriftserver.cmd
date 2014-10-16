SET CurrentDir=%~dp0
SET BinDir=thriftserver\
SET Port=823
SET DictionaryPath=Data\test.in

CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.Thrift.Server.exe -r %DictionaryPath% -p %Port%