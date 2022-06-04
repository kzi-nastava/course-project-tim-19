public class RejectedMedicine
{
    public Medicine medicine{get;set;}
    public string comment{ get; set; }

    public RejectedMedicine(Medicine medicine, string comment){
        this.medicine=medicine;
        this.comment = comment;
    }

    public override string ToString()
    {
        string allergens = "";
        foreach (var allergen in allergens){
            allergens += allergen + " ";
        }
        return "Medicine [id "+medicine+", comment: " + comment +"]";
    }

}