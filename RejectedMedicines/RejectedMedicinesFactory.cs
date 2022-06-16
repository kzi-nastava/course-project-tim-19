using Newtonsoft.Json;
public class RejectedMedicinesFactory{
    public static List<RejectedMedicine> allRejectedMedicines { get; set; } = null!;

    public RejectedMedicinesFactory(){
        var rejectedMedicines = JsonConvert.DeserializeObject<List<RejectedMedicine>>(File.ReadAllText("Data/rejectedMedicine"));
        if (rejectedMedicines != null){
            allRejectedMedicines = rejectedMedicines;
        }
    }

    public static void UpdateData(){
        var convertedRejectedMedicines = JsonConvert.SerializeObject(allRejectedMedicines, Formatting.Indented);
        File.WriteAllText("Data/rejectedMedicines.json", convertedRejectedMedicines);
    }

    public List<RejectedMedicine> GetAllRejectedMedicines(){
        return allRejectedMedicines;
    }

}
