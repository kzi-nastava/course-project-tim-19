using Newtonsoft.Json;
public class RejectedMedicinesFacotry{
    public List<RejectedMedicine> allRejectedMedicines { get; set; } = null!;

    public RejectedMedicinesFactory(string path){
        var rejectedMedicines = JsonConvert.DeserializeObject<List<rejectedMedicine>>(File.ReadAllText(path));
        if (rejectedMedicines != null){
            allRejectedMedicines = rejectedMedicines;
        }
    }

    public static void UpdateRejectedMedicines(List<RejectedMedicine> allRejectedMedicines){
        var convertedRejectedMedicines = JsonConvert.SerializeObject(allRejectedMedicines, Formatting.Indented);
        File.WriteAllText("Data/rejectedMedicines.json", convertedRejectedMedicines);
    }

}