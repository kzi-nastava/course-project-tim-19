using Newtonsoft.Json;


public class UsedEquipment{

    public string name { get; set; }
    public int quantity { get; set; }
    public int roomId { get; set; }

    public UsedEquipment(string name, int quantity,int roomId){
        this.name = name;
        this.quantity = quantity;
        this.roomId = roomId;
    }

    public override string ToString()
    {
        return "UsedEquipment [name: " + name + ", quantity: " + quantity  +", roomId"+roomId+ "]";
    }
}