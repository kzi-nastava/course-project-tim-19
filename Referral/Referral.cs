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
}