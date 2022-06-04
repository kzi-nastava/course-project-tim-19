using Newtonsoft.Json;

public enum EquipmentType{
    SterileGauze,
    Hanzaplast,
    Injection,
    Bandage,
    SterileGloves,
    PainKiller
} 

public class DynamicEquipment{

    public int id { get; set; }
    public EquipmentType type { get; set; }
    public int quantity { get; set; }

    public DynamicEquipment(int id, EquipmentType type, int quantity){
        this.id = id;
        this.type = type;
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return "DynamicEquipment [id: " + id + ", type: " + type + ", quantity: " + quantity  + "]";
    }

    public static void UpdateData(List<DynamicEquipment> allEquipment){
        var convertedEquipment = JsonConvert.SerializeObject(allEquipment, Formatting.Indented);
        File.WriteAllText("Data/dynamicEquipment.json", convertedEquipment);
    }

    const int MAX_DYNAMIC_EQUIPMENT = 100; //can be changed

    public static int FindNextIdForEquipment(List<DynamicEquipment> allEquipment){
        List<int> allIds = new List<int>();
        foreach (DynamicEquipment equipment in allEquipment){
            allIds.Add(equipment.id);
        }
        for (int i = 0; i < MAX_DYNAMIC_EQUIPMENT; ++i){
            if (!allIds.Contains(i)){
                return i;
            }
        }
        return 0;
    }

    public static DynamicEquipment FindById(int id, List<DynamicEquipment> allEquipment){
        foreach(DynamicEquipment equipment in allEquipment){
            if(equipment.id == id){
                return equipment;
            }
        }
        return null;
    }

}