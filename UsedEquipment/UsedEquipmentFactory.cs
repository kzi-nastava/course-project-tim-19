using Newtonsoft.Json;
public class UsedEquipmentFacotry{
    public List<UsedEquipment> allUsedEquipment { get; set; } = null!;

    public UsedEquipmentFacotry(string path){
        var usedEquipments = JsonConvert.DeserializeObject<List<UsedEquipment>>(File.ReadAllText(path));
        if (usedEquipments != null){
            allUsedEquipment = usedEquipments;
        }
    }

    public static void UpdateUsedEquipment(List<RejectedMedicine> allUsedEquipment){
        var convertedUsedEquipments = JsonConvert.SerializeObject(allUsedEquipment, Formatting.Indented);
        File.WriteAllText("Data/usedEquipments.json", convertedUsedEquipments);
    }

}