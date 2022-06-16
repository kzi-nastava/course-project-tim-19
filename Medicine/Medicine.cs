public class Medicine
{
    public int id{get;set;}
    public string name{ get; set; }
    public List<string> allergens { get; set; }
    public string description { get; set; }

    public Medicine(int id,string name, List<string> allergens, string description){
        this.id=id;
        this.name = name;
        this.allergens = allergens;
        this.description = description;
    }

    public override string ToString()
    {
        string allergens = "";
        foreach (var allergen in allergens){
            allergens += allergen + " ";
        }
        return "Medicine [id "+id+", name: " + name + ", allergens: [" + allergens + "], description: " + description + "]";
    }
}