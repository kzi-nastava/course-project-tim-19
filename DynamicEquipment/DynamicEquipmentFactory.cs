using Newtonsoft.Json;
public class DynamicEquipmentFactory{
    public static List<DynamicEquipment> allDynamicEquipment { get; set; } = null!;

    public DynamicEquipmentFactory(){
        var equipment = JsonConvert.DeserializeObject<List<DynamicEquipment>>(File.ReadAllText("Data/dynamicEquipment.json"));
        if (equipment != null){
            allDynamicEquipment = equipment;
        }
    }

    public List<DynamicEquipment> GetAllDynamicEquipment(){
        return allDynamicEquipment;
    }

    public void UpdateData(){
        var convertedEquipment = JsonConvert.SerializeObject(allDynamicEquipment, Formatting.Indented);
        File.WriteAllText("Data/dynamicEquipment.json", convertedEquipment);
    }

    public int FindNextIdForEquipment(){
        List<int> allIds = new List<int>();
        foreach (DynamicEquipment equipment in allDynamicEquipment){
            allIds.Add(equipment.id);
        }
        for (int i = 0; i < 100; ++i){
            if (!allIds.Contains(i)){
                return i;
            }
        }
        return 0;
    }

     public DynamicEquipment FindById(int id){
        foreach(DynamicEquipment equipment in allDynamicEquipment){
            if(equipment.id == id){
                return equipment;
            }
        }
        return null;
    }

    public List<DynamicEquipment> FindAllEquipmentByIds(List<int> equipmentIds){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<DynamicEquipment> foundEquipment = new List<DynamicEquipment>();
        foreach (int id in equipmentIds){
            DynamicEquipment equipment = dynamicEquipmentFactory.FindById(id);
            foundEquipment.Add(equipment);
        }
        return foundEquipment;
    }

    public List<int> CountTheEquipment(){
        List<int> countedEquipment = new List<int>();
        int sterileGauzes = 0, hanzaplasts = 0, injections = 0, bandages = 0, sterileGloves = 0, painKillers = 0;
        foreach(DynamicEquipment equipment in allDynamicEquipment){
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

    public List<EquipmentType> FindMissingEquipment(){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<int> countedEquipment = dynamicEquipmentFactory.CountTheEquipment();
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

    public void OrderMissingEquipment(List<Room> allRooms){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<EquipmentType> missingEquipment = dynamicEquipmentFactory.FindMissingEquipment();
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
                DynamicEquipment newEquipment = new DynamicEquipment(dynamicEquipmentFactory.FindNextIdForEquipment(), missingEquipment[Convert.ToInt32(selectedEquipment)-1], Convert.ToInt32(amountOfEquipment));
                Console.WriteLine("Request for ordering " + newEquipment.type + " is created!");
                allDynamicEquipment.Add(newEquipment);
                dynamicEquipmentFactory.UpdateData();
                DynamicEquipment.AddToStorageRoom(newEquipment);
            }
        }
    }

}