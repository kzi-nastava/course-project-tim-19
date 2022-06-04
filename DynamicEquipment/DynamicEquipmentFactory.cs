using Newtonsoft.Json;
public class DynamicEquipmentFactory{
    public List<DynamicEquipment> allEquipment { get; set; } = null!;

    public DynamicEquipmentFactory(string path){
        var equipment = JsonConvert.DeserializeObject<List<DynamicEquipment>>(File.ReadAllText(path));
        if (equipment != null){
            allEquipment = equipment;
        }
    }
}