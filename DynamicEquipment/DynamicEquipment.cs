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


    public static void AddToStorageRoom(DynamicEquipment newEquipment){
        RoomFactory roomFactory = new RoomFactory();
        roomFactory.GetAllRooms()[0].equipmentIds.Add(newEquipment.id);
        roomFactory.UpdateData();
    }
    
    public static Dictionary<EquipmentType, int> FindLowEquipment(){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<int> countedEquipment = dynamicEquipmentFactory.CountTheEquipment();
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

    public static void ViewAllRoomsWithMissingEquipment(List<Room> roomsWithMissingEquipment){
        foreach(Room room in roomsWithMissingEquipment){
                Console.WriteLine(room);
        }
    }

    public static void RearrangeInRoomWithMissingEquipment(List<Room> roomsWithMissingEquipment){
        RoomFactory roomFactory = new RoomFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        Console.WriteLine("Rooms with missing equipment:");
        ViewAllRoomsWithMissingEquipment(roomsWithMissingEquipment);
        Console.WriteLine("Enter id of the room to see what equipment is missing:");
        var roomsId = Console.ReadLine();
        Room chosenRoom = roomFactory.FindById(Convert.ToInt32(roomsId));
        List<DynamicEquipment> equipmentInTheRoom =  dynamicEquipmentFactory.FindAllEquipmentByIds(chosenRoom.equipmentIds);
        List<EquipmentType> missingEquipment = dynamicEquipmentFactory.FindMissingEquipment();
        int i = 0;
        foreach(EquipmentType equipment in missingEquipment){
            Console.WriteLine(i+1 + ". type:" + equipment);
            i++;
        }
        Console.WriteLine("Enter the option for equipment you want to add:");
        var optionForEquipment = Console.ReadLine();
        EquipmentType equipmentType = missingEquipment[Convert.ToInt32(optionForEquipment)-1];
        roomFactory.FindEquipmentTypeInRooms(equipmentType, dynamicEquipmentFactory.GetAllDynamicEquipment());
        roomFactory.MoveEquipment(dynamicEquipmentFactory.GetAllDynamicEquipment(), chosenRoom);
    }

    public static void RearrangeInRoomsWithLowEquipment(List<Room> roomsWithLowEquipment){
        RoomFactory roomFactory = new RoomFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        Console.WriteLine("Rooms with equipment quantity less than 5:");
            foreach(Room room in roomsWithLowEquipment){
                Console.WriteLine(room);
            }
            Console.WriteLine("Enter id of the room to see what equipment has quantity lower than 5:");
            var roomsId = Console.ReadLine();
            Room chosenRoom = roomFactory.FindById(Convert.ToInt32(roomsId));
            List<DynamicEquipment> equipmentInTheRoom = dynamicEquipmentFactory.FindAllEquipmentByIds(chosenRoom.equipmentIds);
            Dictionary<EquipmentType, int> lowEquipment = FindLowEquipment();
            int i = 0;
            foreach (var equipment in lowEquipment){
                Console.WriteLine(i+1 + ". type: " + equipment.Key + ", quantity: " + equipment.Value);
                i++;
            }
            Console.WriteLine("Enter the option for equipment you want to add:");
            var optionForEquipment = Console.ReadLine();
            EquipmentType equipmentType = lowEquipment.ElementAt(Convert.ToInt32(optionForEquipment)-1).Key;
            roomFactory.FindEquipmentTypeInRooms(equipmentType, dynamicEquipmentFactory.GetAllDynamicEquipment());
            roomFactory.MoveEquipment(dynamicEquipmentFactory.GetAllDynamicEquipment(), chosenRoom);
    }

    public static void RearrangeTheEquipment(List<Room> allRooms, List<DynamicEquipment> allEquipment){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomFactory = new RoomFactory();
        List<Room> roomsWithMissingEquipment = roomFactory.FindRoomsWithMissingEquipment(allEquipment);
        List<Room> roomsWithLowEquipment = roomFactory.FindRoomsWithLowEquipment(allEquipment);
        Console.WriteLine("Do you want to rearrange equipment in rooms with missing equipment or rooms with equipment quantity less then 5?");
        Console.WriteLine("1. missing equipment\n2. low equipment");
        Console.WriteLine("Enter the option:");
        var optionForRooms = Console.ReadLine();
        if (optionForRooms == "1"){
            RearrangeInRoomWithMissingEquipment(roomsWithMissingEquipment);
        } else if (optionForRooms == "2"){
            RearrangeInRoomsWithLowEquipment(roomsWithLowEquipment);
        } else {
            Console.WriteLine("You entered the wrong option!");
        }
    }


}
