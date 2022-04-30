public class Recipe
{
    public int id{ get; set; }
    public List<Medicine> medicines { get; set; }
    public List<int> perDay { get; set; }
    public int duration { get; set; } //drugi naziv

    public Recipe(int id, List<Medicine> medicines, List<int> perDay, int duration){
        this.id = id;
        this.medicines = medicines;
        this.perDay = perDay;
        this.duration = duration;
    }

    public override string ToString()
    {
        string medicines = "";
        foreach (var medicine in medicines){
            medicines += medicine.ToString() + " ";
        }
        string perDays = "";
        foreach (int perDay in perDay){
            perDays += perDay + " ";
        }
        return "Recipe [id: " + id + ", medicines: [" + medicines + "], per day: [" + perDays + "], duration: " + duration + "]";
    }

}