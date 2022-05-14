using Newtonsoft.Json;
public class RecipesFactory{
    public List<Recipe> allRecipes { get; set; } = null!;

    public RecipesFactory(string path){
        var recipes = JsonConvert.DeserializeObject<List<Recipe>>(File.ReadAllText(path));
        if (recipes != null){
            allRecipes = recipes;
        }
    }

    public static void UpdateRecipes(List<Recipe> allRecipes){
        var convertedRecipes = JsonConvert.SerializeObject(allRecipes, Formatting.Indented);
        File.WriteAllText("Data/recipes.json", convertedRecipes);
    }
}