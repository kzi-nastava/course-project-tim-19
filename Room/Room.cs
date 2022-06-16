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
    
}
