namespace csharp Vdk.AutoCompleter.Thrift.Core
service AutoCompleteService {   
   list<string> get(1:string prefix)   
}