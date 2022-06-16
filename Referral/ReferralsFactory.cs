using Newtonsoft.Json;
public class ReferralsFactory{
    public static List<Referral> allRefferals { get; set; } = null!;

    public ReferralsFactory(){
        var referrals = JsonConvert.DeserializeObject<List<Referral>>(File.ReadAllText("Data/referrals.json"));
        if (referrals != null){
            allRefferals = referrals;
        }
    }

    public void UpdateData(){
        var convertedReferrals = JsonConvert.SerializeObject(allRefferals, Formatting.Indented);
        File.WriteAllText("Data/referrals.json", convertedReferrals);
    }

    public Referral FindById(int id){
        foreach (Referral referral in allRefferals){
            if (referral.referralsId == id){
                return referral;
            }
        }
        return null;
    }

    public static Referral FindReferralByPatient(List<Patient> allPatients){
        PatientsFactory patientsFactory = new PatientsFactory();
        ReferralsFactory referralsFactory = new ReferralsFactory();
        Console.WriteLine("Enter patient's id: ");
        var patientsId = Console.ReadLine();
        Patient patient = patientsFactory.FindById(Convert.ToInt32(patientsId));
        Referral referral = referralsFactory.FindById(patient.referralsId);
        Console.WriteLine(referral);
        return referral;
    }

    public List<Referral> GetAllReferrals(){
        return allRefferals;
    }
}