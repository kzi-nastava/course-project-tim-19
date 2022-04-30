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
    public List<Recipe> recipes { get; set; }

    public Patient(int id, string name, string email, string password, MedicalRecord medicalRecord, Blocked blocked,List<Recipe> recipes){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.medicalRecord = medicalRecord;
        this.blocked = blocked;
        this.recipes = recipes;
    }

    public override string ToString()
    {
        string allRecipes = "";
        foreach (var recipe in recipes){
            allRecipes += recipe.ToString();
        }
        return "Patient [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + ", medicalRecord: " + medicalRecord + ", blocked: " + blocked + ", recipes: [" + allRecipes + "]]";
    }

    
}