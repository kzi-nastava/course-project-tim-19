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
    
    public static Dictionary<EquipmentType, int> FindLowEquipment(List<DynamicEquipment> allEquipment){
        List<int> countedEquipment = CountTheEquipment(allEquipment);
        Dictionary<EquipmentType, int> lowEquipment = new Dictionary<EquipmentType, int>();
        if (countedEquipment[0] < 5){
            lowEquipment.Add((EquipmentType)0, countedEquipment[0]);
        } else if (countedEquipment[1] < 5){
            lowEquipment.Add((EquipmentType)1, countedEquipment[1]);
        } else if (countedEquipment[2] < 5){
            lowEquipment.Add((EquipmentType)2, countedEquipment[2]);
        } else if (countedEquipment[3] < 5){
            lowEquipment.Add((EquipmentType)3, countedEquipment[3]);
        } else if (countedEquipment[4] < 5){
            lowEquipment.Add((EquipmentType)4, countedEquipment[4]);
        } else if (countedEquipment[5] < 5){
            lowEquipment.Add((EquipmentType)5, countedEquipment[5]);
        }
        return lowEquipment;
    }

    public static void RearrangeTheEquipment(List<Room> allRooms, List<DynamicEquipment> allEquipment){
        List<Room> roomsWithMissingEquipment = Room.FindRoomsWithMissingEquipment(allRooms, allEquipment);
        List<Room> roomsWithLowEquipment = Room.FindRoomsWithLowEquipment(allRooms, allEquipment);
        Console.WriteLine("Do you want to rearrange equipment in rooms with missing equipment or rooms with equipment quantity less then 5?");
        Console.WriteLine("1. missing equipment\n2. low equipment");
        Console.WriteLine("Enter the option:");
        var optionForRooms = Console.ReadLine();
        if (optionForRooms == "1"){
            Console.WriteLine("Rooms with missing equipment:");
            foreach(Room room in roomsWithMissingEquipment){
                Console.WriteLine(room);
            }
            Console.WriteLine("Enter id of the room to see what equipment is missing:");
            var roomsId = Console.ReadLine();
            Room chosenRoom = Room.FindById(Convert.ToInt32(roomsId), allRooms);
            List<DynamicEquipment> equipmentInTheRoom = FindAllEquipmentByIds(chosenRoom.equipmentIds, allEquipment);
            List<EquipmentType> missingEquipment = FindMissingEquipment(equipmentInTheRoom);
            int i = 0;
            foreach(EquipmentType equipment in missingEquipment){
                Console.WriteLine(i+1 + ". type:" + equipment);
                i++;
            }
            Console.WriteLine("Enter the option for equipment you want to add:");
            var optionForEquipment = Console.ReadLine();
            EquipmentType equipmentType = missingEquipment[Convert.ToInt32(optionForEquipment)-1];
            Room.FindEquipmentTypeInRooms(equipmentType, allRooms, allEquipment);
            Room.MoveEquipment(allEquipment, chosenRoom, allRooms);
        } else if (optionForRooms == "2"){
            Console.WriteLine("Rooms with equipment quantity less than 5:");
            foreach(Room room in roomsWithLowEquipment){
                Console.WriteLine(room);
            }
            Console.WriteLine("Enter id of the room to see what equipment has quantity lower than 5:");
            var roomsId = Console.ReadLine();
            Room chosenRoom = Room.FindById(Convert.ToInt32(roomsId), allRooms);
            List<DynamicEquipment> equipmentInTheRoom = FindAllEquipmentByIds(chosenRoom.equipmentIds, allEquipment);
            Dictionary<EquipmentType, int> lowEquipment = FindLowEquipment(equipmentInTheRoom);
            int i = 0;
            foreach (var equipment in lowEquipment){
                Console.WriteLine(i+1 + ". type: " + equipment.Key + ", quantity: " + equipment.Value);
                i++;
            }
            Console.WriteLine("Enter the option for equipment you want to add:");
            var optionForEquipment = Console.ReadLine();
            EquipmentType equipmentType = lowEquipment.ElementAt(Convert.ToInt32(optionForEquipment)-1).Key;
            Room.FindEquipmentTypeInRooms(equipmentType, allRooms, allEquipment);
            Room.MoveEquipment(allEquipment, chosenRoom, allRooms);
        } else {
            Console.WriteLine("You entered the wrong option!");
        }
    }


}
