using Newtonsoft.Json;
public class MedicinesFactory{
    public static List<Medicine> allMedicines { get; set; } = null!;

    public MedicinesFactory(){
        var medicines = JsonConvert.DeserializeObject<List<Medicine>>(File.ReadAllText("Data/medicines.json"));
        if (medicines != null){
            allMedicines = medicines;
        }
    }

    public void UpdateData(){
        var convertedMedicines = JsonConvert.SerializeObject(allMedicines, Formatting.Indented);
        File.WriteAllText("Data/medicineRequests.json", convertedMedicines);
    }

    public List<Medicine> GetAllMedicines(){
        return allMedicines;
    }

}