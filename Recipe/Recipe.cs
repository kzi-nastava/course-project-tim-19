public class Recipe
{
    public int id{ get; set; }
    public List<int> medicines { get; set; }
    public List<int> perDay { get; set; }
    public int duration { get; set; } 

    public Recipe(int id, List<int> medicines, List<int> perDay, int duration){
        this.id = id;
        this.medicines = medicines;
        this.perDay = perDay;
        this.duration = duration;
    }

    public override string ToString()
    {
        return "Recipe id: " + id ;
    }

}