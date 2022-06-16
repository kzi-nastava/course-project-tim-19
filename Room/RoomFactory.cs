using Newtonsoft.Json;
public class RoomFactory{
    public static List<Room> allRooms { get; set; } = null!;

    public RoomFactory(){
        var rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText("Data/rooms.json"));
        if (rooms != null){
            allRooms = rooms;
        }
    }

    public List<Room> GetAllRooms(){
        return allRooms;
    }

    public Room FindById(int id){
        foreach(Room room in allRooms){
            if(room.id == id){
                return room;
            }
        }
        return null;
    }

    public void UpdateData(){
        var convertedRooms = JsonConvert.SerializeObject(allRooms, Formatting.Indented);
        File.WriteAllText("Data/rooms.json", convertedRooms);
    }

    public List<Room> FindRoomsWithMissingEquipment(List<DynamicEquipment> allEquipment){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<Room> roomsWithMissingEquipment = new List<Room>();
        foreach(Room room in allRooms){
            List<DynamicEquipment> equipmentInTheRoom = dynamicEquipmentFactory.FindAllEquipmentByIds(room.equipmentIds);
            List<EquipmentType> missingEquipment = dynamicEquipmentFactory.FindMissingEquipment();
            if (missingEquipment.Count != 0){
                roomsWithMissingEquipment.Add(room);
            }
        }
        return roomsWithMissingEquipment;
    }

    public List<Room> FindRoomsWithLowEquipment(List<DynamicEquipment> allEquipment){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        List<Room> roomsWithLowEquipment = new List<Room>();
        foreach(Room room in allRooms){
            List<DynamicEquipment> equipmentInTheRoom = dynamicEquipmentFactory.FindAllEquipmentByIds(room.equipmentIds);
            Dictionary<EquipmentType, int> lowEquipment = DynamicEquipment.FindLowEquipment();
            if (lowEquipment.Count != 0){
                roomsWithLowEquipment.Add(room);
            }
        }
        return roomsWithLowEquipment;
    }

    public void FindEquipmentTypeInRooms(EquipmentType equipmentType, List<DynamicEquipment> allEquipment){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        foreach(Room room in allRooms){
            List<DynamicEquipment> roomsEquipment = dynamicEquipmentFactory.FindAllEquipmentByIds(room.equipmentIds);
            List<DynamicEquipment> foundEquipment = new List<DynamicEquipment>();
            foreach(DynamicEquipment equipment in roomsEquipment){
                if (equipment.type == equipmentType){
                    foundEquipment.Add(equipment);
                }
            }
            if(foundEquipment.Count != 0){
                Console.WriteLine(room.name);
                foreach(DynamicEquipment equipment in foundEquipment){
                    Console.WriteLine(equipment);
                }
            }
        }
        
    }

    public void MoveEquipment(List<DynamicEquipment> allEquipment, Room room){
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomsFactory = new RoomFactory();
        Console.WriteLine("Enter the equipment's id you want to move:");
        var equipmentId = Console.ReadLine();
        DynamicEquipment equipmentToMove = dynamicEquipmentFactory.FindById(Convert.ToInt32(equipmentId));
        if (equipmentToMove == null) {
            Console.WriteLine("Wrong id!");
        } else {
            Console.WriteLine("Enter the amount you want to transfer: ");
            var amountOfEquipment = Console.ReadLine();
            if (equipmentToMove.quantity < Convert.ToInt32(amountOfEquipment)){
                Console.WriteLine("There's not enough equipment!");
                return;
            }
            DynamicEquipment movedEquipment = new DynamicEquipment(dynamicEquipmentFactory.FindNextIdForEquipment(), equipmentToMove.type, Convert.ToInt32(amountOfEquipment));
            allEquipment.Add(movedEquipment);
            room.equipmentIds.Add(movedEquipment.id);
            equipmentToMove.quantity -= Convert.ToInt32(amountOfEquipment);
            // if (equipmentToMove.quantity == 0){
            //     allEquipment.Remove(equipmentToMove);
            // }
            roomsFactory.UpdateData();
            dynamicEquipmentFactory.UpdateData();
            Console.WriteLine("You successfully moved equipment!");
        }
    }

}