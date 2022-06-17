using Newtonsoft.Json;
public class RecipesFactory{
    public static List<Recipe> allRecipes { get; set; } = null!;

    public RecipesFactory(){
        var recipes = JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText("Data/recipes.json"));
        if (recipes != null){
            allRecipes = recipes;
        }
    }

    public void UpdateData(){
        var convertedRecipes = JsonConvert.SerializeObject(allRecipes, Formatting.Indented);
        File.WriteAllText("Data/recipes.json", convertedRecipes);
    }

    public List<Recipe> GetAllRecipes(){
        return allRecipes;
    }
}