namespace csharp Vdk.AutoCompleter.Thrift.Core
service AutoCompleteService {   
   list<string> Get(1:string prefix)   
}