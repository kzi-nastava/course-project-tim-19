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
    
}