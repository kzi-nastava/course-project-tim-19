public class MedicalRecord
{
    public int height{ get; set; }
    public float weight { get; set; }
    public string bloodType { get; set; }
    public List<string> previousIllnesses { get; set; }
    public List<string> allergens { get; set; }

    public MedicalRecord(int height, float weight, string bloodType,List<string> previousIllnesses,List<string> allergens){
        this.height = height;
        this.weight = weight;
        this.bloodType = bloodType;
        this.previousIllnesses = previousIllnesses;
        this.allergens = allergens;
    }

    public override string ToString()
    {
        string illnesses = "";
        foreach (var illness in previousIllnesses){
            illnesses += illness + " ";
        }
        string allergens = "";
        foreach (var allergen in allergens){
            allergens += allergen + " ";
        }
        return "MedicalRecord [height: " + height + "cm, weight: " + weight + "kg, blood type: " + bloodType + ", previous illnesses: [" + illnesses + "], allergens: [" + allergens + "]]"; 
    }

}