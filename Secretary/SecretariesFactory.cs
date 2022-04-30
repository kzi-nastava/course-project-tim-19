using Newtonsoft.Json;
public class SecretariesFactory{
    public List<Secretary> allSecretaries { get; set; } = null!;

    public SecretariesFactory(string path){
        var secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(path));
        if (secretaries != null){
            allSecretaries = secretaries;
        }
    }
}