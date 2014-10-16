SET CurrentDir=%~dp0
SET BinDir=wcfserver\
SET Port=813
SET DictionaryPath=Data\test.in

CD %CurrentDir%%BinDir%
Vdk.AutoCompleter.Wcf.Server.exe -r %DictionaryPath% -p %Port%