using Newtonsoft.Json;
public class SecretariesFactory{
    public static List<Secretary> allSecretaries { get; set; } = null!;

    public SecretariesFactory(){
        var secretaries = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText("Data/secretaries.json"));
        if (secretaries != null){
            allSecretaries = secretaries;
        }
    }

    public List<Secretary> GetAllSecretaries(){
        return allSecretaries;
    }

}