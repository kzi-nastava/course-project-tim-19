using Newtonsoft.Json;
public class ModificationRequestsFactory{
    public List<ModificationRequest> allModificationRequests { get; set; } = null!;

    public ModificationRequestsFactory(string path){
        var modificationRequests = JsonConvert.DeserializeObject<List<ModificationRequest>>(File.ReadAllText(path));
        if (modificationRequests != null){
            allModificationRequests = modificationRequests;
        }
    }
}