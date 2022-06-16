using Newtonsoft.Json;
public class ReferralsFactory{
    public static List<Referral> allRefferals { get; set; } = null!;

    public ReferralsFactory(string path){
        var referrals = JsonConvert.DeserializeObject<List<Referral>>(File.ReadAllText(path));
        if (referrals != null){
            allRefferals = referrals;
        }
    }

    public void UpdateData(){
        var convertedReferrals = JsonConvert.SerializeObject(allRefferals, Formatting.Indented);
        File.WriteAllText("Data/referrals.json", convertedReferrals);
    }

    public List<Referral> GetAllReferrals(){
        return allRefferals;
    }
}