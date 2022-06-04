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

    public static void UpdateMedicineRequests(List<Medicine> allMedicines){ //NOVO
        var convertedMedicines = JsonConvert.SerializeObject(allMedicines, Formatting.Indented);
        File.WriteAllText("Data/medicineRequests.json", convertedMedicines);
    }
}