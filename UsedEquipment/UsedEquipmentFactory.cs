using Newtonsoft.Json;
public class UsedEquipmentFactory{
    public static List<UsedEquipment> allUsedEquipment { get; set; } = null!;

    public UsedEquipmentFactory(){
        var usedEquipments = JsonConvert.DeserializeObject<List<UsedEquipment>>(File.ReadAllText("Data/usedEquipment.json"));
        if (usedEquipments != null){
            allUsedEquipment = usedEquipments;
        }
    }
    public List<UsedEquipment> GetAllUsedEquipment(){
        return allUsedEquipment;
    }
    public void UpdateData(){
        var convertedUsedEquipments = JsonConvert.SerializeObject(allUsedEquipment, Formatting.Indented);
        File.WriteAllText("Data/usedEquipments.json", convertedUsedEquipments);
    }

}