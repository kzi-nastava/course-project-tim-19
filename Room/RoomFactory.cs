using Newtonsoft.Json;
public class RoomFactory{
    public List<Room> allRooms { get; set; } = null!;

    public RoomFactory(string path){
        var rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(path));
        if (rooms != null){
            allRooms = rooms;
        }
    }
}