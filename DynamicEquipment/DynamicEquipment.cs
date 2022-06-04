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

    public static List<DynamicEquipment> FindAllEquipmentByIds(List<int> equipmentIds, List<DynamicEquipment> allEquipment){
        List<DynamicEquipment> foundEquipment = new List<DynamicEquipment>();
        foreach (int id in equipmentIds){
            DynamicEquipment equipment = FindById(id, allEquipment);
            foundEquipment.Add(equipment);
        }
        return foundEquipment;
    }

    public static List<int> CountTheEquipment(List<DynamicEquipment> allEquipment){
        List<int> countedEquipment = new List<int>();
        int sterileGauzes = 0, hanzaplasts = 0, injections = 0, bandages = 0, sterileGloves = 0, painKillers = 0;
        foreach(DynamicEquipment equipment in allEquipment){
            if (equipment.type == (EquipmentType)0){
                sterileGauzes++;
            } else if (equipment.type == (EquipmentType)1){
                hanzaplasts++;
            } else if (equipment.type == (EquipmentType)2){
                injections++;
            } else if (equipment.type == (EquipmentType)3){
                bandages++;
            } else if (equipment.type == (EquipmentType)4){
                sterileGloves++;
            } else {
                painKillers++;
            }
        }
        countedEquipment.Add(sterileGauzes);
        countedEquipment.Add(hanzaplasts);
        countedEquipment.Add(injections);
        countedEquipment.Add(bandages);
        countedEquipment.Add(sterileGloves);
        countedEquipment.Add(painKillers);
        return countedEquipment;
    }

    public static List<EquipmentType> FindMissingEquipment(List<DynamicEquipment> allEquipment){
        List<int> countedEquipment = CountTheEquipment(allEquipment);
        List<EquipmentType> missingEquipment = new List<EquipmentType>();
        if (countedEquipment[0] == 0){
            missingEquipment.Add((EquipmentType)0);
        } else if (countedEquipment[1] == 0){
            missingEquipment.Add((EquipmentType)1);
        } else if (countedEquipment[2] == 0){
            missingEquipment.Add((EquipmentType)2);
        } else if (countedEquipment[3] == 0){
            missingEquipment.Add((EquipmentType)3);
        } else if (countedEquipment[4] == 0){
            missingEquipment.Add((EquipmentType)4);
        } else if (countedEquipment[5] == 0){
            missingEquipment.Add((EquipmentType)5);
        }
        return missingEquipment;
    }

    public static void AddToStorageRoom(DynamicEquipment newEquipment, List<Room> allRooms){
        allRooms[0].equipmentIds.Add(newEquipment.id);
        Room.UpdateData(allRooms);
    }

    public static void OrderMissingEquipment(List<DynamicEquipment> allEquipment, List<Room> allRooms){
        List<EquipmentType> missingEquipment = FindMissingEquipment(allEquipment);
        if (missingEquipment.Count == 0){
            Console.WriteLine("There's no missing equipment!");
        } else {
            Console.WriteLine("Missing equipment:");
            for (int i = 0; i < missingEquipment.Count; i++){
                Console.WriteLine(i+1 + ". " + missingEquipment[i]);
            }
            Console.WriteLine("Enter what equipment you want to order (or x for going back):");
            var selectedEquipment = Console.ReadLine();
            if (selectedEquipment == "x"){
                return;
            } else {
                Console.WriteLine("Enter the amount you want to order:");
                var amountOfEquipment = Console.ReadLine();
                DynamicEquipment newEquipment = new DynamicEquipment(FindNextIdForEquipment(allEquipment), missingEquipment[Convert.ToInt32(selectedEquipment)-1], Convert.ToInt32(amountOfEquipment));
                Console.WriteLine("Request for ordering " + newEquipment.type + " is created!");
                allEquipment.Add(newEquipment);
                UpdateData(allEquipment);
                AddToStorageRoom(newEquipment, allRooms);
            }
        }
    }


}