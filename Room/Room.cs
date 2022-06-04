using Newtonsoft.Json;

public class Room{

    public int id { get; set; }
    public String name { get; set; }
    public List<int> equipmentIds { get; set; }

    public Room(int id, String name){
        List<int> equipmentsId = new List<int>();
        this.id = id;
        this.name = name;
        this.equipmentIds = equipmentIds;
    }

    public override string ToString()
    {
        return "Room [id: " + id + ", name: " + name + "]";
    }

    public static Room FindById(int id, List<Room> allRooms){
        foreach(Room room in allRooms){
            if(room.id == id){
                return room;
            }
        }
        return null;
    }

    public static void UpdateData(List<Room> allRooms){
        var convertedRooms = JsonConvert.SerializeObject(allRooms, Formatting.Indented);
        File.WriteAllText("Data/rooms.json", convertedRooms);
    }
    
    public static Room FindRoomById(int id, List<Room> allRooms){ //remove later
        foreach(Room room in allRooms){
            if(room.id == id){
                return room;
            }
        }
        return null;
    }
    
    public static List<Room> FindRoomsWithMissingEquipment(List<Room> allRooms, List<DynamicEquipment> allEquipment){
        List<Room> roomsWithMissingEquipment = new List<Room>();
        foreach(Room room in allRooms){
            List<DynamicEquipment> equipmentInTheRoom = DynamicEquipment.FindAllEquipmentByIds(room.equipmentIds, allEquipment);
            List<EquipmentType> missingEquipment = DynamicEquipment.FindMissingEquipment(equipmentInTheRoom);
            if (missingEquipment.Count != 0){
                roomsWithMissingEquipment.Add(room);
            }
        }
        return roomsWithMissingEquipment;
    }

    public static List<Room> FindRoomsWithLowEquipment(List<Room> allRooms, List<DynamicEquipment> allEquipment){
        List<Room> roomsWithLowEquipment = new List<Room>();
        foreach(Room room in allRooms){
            List<DynamicEquipment> equipmentInTheRoom = DynamicEquipment.FindAllEquipmentByIds(room.equipmentIds, allEquipment);
            Dictionary<EquipmentType, int> lowEquipment = DynamicEquipment.FindLowEquipment(equipmentInTheRoom);
            if (lowEquipment.Count != 0){
                roomsWithLowEquipment.Add(room);
            }
        }
        return roomsWithLowEquipment;
    }

    public static void FindEquipmentTypeInRooms(EquipmentType equipmentType,List<Room> allRooms, List<DynamicEquipment> allEquipment){
        foreach(Room room in allRooms){
            List<DynamicEquipment> roomsEquipment = DynamicEquipment.FindAllEquipmentByIds(room.equipmentIds, allEquipment);
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

    public static void MoveEquipment(List<DynamicEquipment> allEquipment, Room room, List<Room> allRooms){
        Console.WriteLine("Enter the equipment's id you want to move:");
        var equipmentId = Console.ReadLine();
        DynamicEquipment equipmentToMove = DynamicEquipment.FindById(Convert.ToInt32(equipmentId), allEquipment);
        if (equipmentToMove == null) {
            Console.WriteLine("Wrong id!");
        } else {
            Console.WriteLine("Enter the amount you want to transfer: ");
            var amountOfEquipment = Console.ReadLine();
            if (equipmentToMove.quantity < Convert.ToInt32(amountOfEquipment)){
                Console.WriteLine("There's not enough equipment!");
                return;
            }
            DynamicEquipment movedEquipment = new DynamicEquipment(DynamicEquipment.FindNextIdForEquipment(allEquipment), equipmentToMove.type, Convert.ToInt32(amountOfEquipment));
            allEquipment.Add(movedEquipment);
            room.equipmentIds.Add(movedEquipment.id);
            equipmentToMove.quantity -= Convert.ToInt32(amountOfEquipment);
            // if (equipmentToMove.quantity == 0){
            //     allEquipment.Remove(equipmentToMove);
            // }
            UpdateData(allRooms);
            DynamicEquipment.UpdateData(allEquipment);
            Console.WriteLine("You successfully moved equipment!");
        }
    }
    
}
