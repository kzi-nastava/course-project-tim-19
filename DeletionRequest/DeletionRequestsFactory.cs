using Newtonsoft.Json;
public class DeletionRequestsFactory{
    public List<DeletionRequest> allDeletionRequests { get; set; } = null!;

    public DeletionRequestsFactory(string path){
        var deletionRequests = JsonConvert.DeserializeObject<List<DeletionRequest>>(File.ReadAllText(path));
        if (deletionRequests != null){
            allDeletionRequests = deletionRequests;
        }
    }
}