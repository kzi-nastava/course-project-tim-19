using Newtonsoft.Json;
public class UsedEquipmentFacotry{
    public static List<UsedEquipment> allUsedEquipment { get; set; } = null!;

    public UsedEquipmentFacotry(){
        var usedEquipments = JsonConvert.DeserializeObject<List<UsedEquipment>>(File.ReadAllText("Data/usedEquipment.json"));
        if (usedEquipments != null){
            allUsedEquipment = usedEquipments;
        }
    }

    public void UpdateData(){
        var convertedUsedEquipments = JsonConvert.SerializeObject(allUsedEquipment, Formatting.Indented);
        File.WriteAllText("Data/usedEquipments.json", convertedUsedEquipments);
    }

}