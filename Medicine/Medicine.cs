public class Medicine
{
    public string name{ get; set; }
    public List<string> allergens { get; set; }
    public string description { get; set; }
    //public int quantity { get; set; }

    public Medicine(string name, List<string> allergens, string description){
        this.name = name;
        this.allergens = allergens;
        this.description = description;
        //quantity = quantity;
    }

    public override string ToString()
    {
        string allergens = "";
        foreach (var allergen in allergens){
            allergens += allergen + " ";
        }
        return "Medicine [name: " + name + ", allergens: [" + allergens + "], description: " + description + "]";
    }
}