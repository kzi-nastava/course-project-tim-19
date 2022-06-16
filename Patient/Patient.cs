using Newtonsoft.Json;
public enum Blocked{
    NotBlocked,
    Block
}

public class Patient
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public MedicalRecord medicalRecord {get; set; }
    public Blocked blocked { get; set; }
    public List<int> recipes { get; set; }
    public int referralsId { get; set; }

    public Patient(int id, string name, string email, string password, MedicalRecord medicalRecord, Blocked blocked,List<int> recipes){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.medicalRecord = medicalRecord;
        this.blocked = blocked;
        this.recipes = recipes;
        this.referralsId = 0;
    }

    public override string ToString()
    {
        string allRecipes = "";
        foreach (int recipe in recipes){
            allRecipes += recipe+" ";
        }
        return "Patient [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + ", medicalRecord: " + medicalRecord + ", blocked: " + blocked + ", recipes: [" + allRecipes + "], referral's id: " + referralsId+ "]";
    }
    
}
