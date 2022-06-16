using Newtonsoft.Json;
public class Referral
{
    public int referralsId{ get; set; }
    public int patientsId{ get; set; }
    public int doctorsId{ get; set; }

    public Referral(int referralsId, int patientsId, int doctorsId){
        this.referralsId = referralsId;
        this.patientsId = patientsId;
        this.doctorsId = doctorsId;
    }

    public override string ToString()
    {
        return "Referral [id: " + referralsId + ", patient's id: " + patientsId + ", doctor's id: " + doctorsId + "]";
    }

    public static Referral FindById(int id){
        ReferralsFactory referrals = new ReferralsFactory("Data/referrals.json");
        foreach (Referral referral in referrals.GetAllReferrals()){
            if (referral.referralsId == id){
                return referral;
            }
        }
        return null;
    }

    public static Referral FindReferralByPatient(List<Patient> allPatients){
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Enter patient's id: ");
        var patientsId = Console.ReadLine();
        Patient patient = patientsFactory.FindById(Convert.ToInt32(patientsId));
        Referral referral = Referral.FindById(patient.referralsId);
        Console.WriteLine(referral);
        return referral;
    }

    public static void UpdateData(List<Referral> allReferrals){
        var convertedReferrals = JsonConvert.SerializeObject(allReferrals, Formatting.Indented);
        File.WriteAllText("Data/referrals.json", convertedReferrals);
    }

}