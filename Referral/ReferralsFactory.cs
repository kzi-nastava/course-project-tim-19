using Newtonsoft.Json;
public class ReferralsFactory{
    public List<Referral> allRefferals { get; set; } = null!;

    public ReferralsFactory(string path){
        var referrals = JsonConvert.DeserializeObject<List<Referral>>(File.ReadAllText(path));
        if (referrals != null){
            allRefferals = referrals;
        }
    }

    public static void UpdateReferrals(List<Referral> allRefferals){
        var convertedReferrals = JsonConvert.SerializeObject(allRefferals, Formatting.Indented);
        File.WriteAllText("Data/referrals.json", convertedReferrals);
    }
}