using Newtonsoft.Json;
public class MedicinesFactory{
    public List<Medicine> allMedicines { get; set; } = null!;

    public MedicinesFactory(string path){
        var medicines = JsonConvert.DeserializeObject<List<Medicine>>(File.ReadAllText(path));
        if (medicines != null){
            allMedicines = medicines;
        }
    }

    public static void UpdateMedicines(List<Medicine> allMedicines){
        var convertedMedicines = JsonConvert.SerializeObject(allMedicines, Formatting.Indented);
        File.WriteAllText("Data/medicines.json", convertedMedicines);
    }
}