using Newtonsoft.Json;
public class DeletionRequest{

    public int appointmentsId;

    public DeletionRequest(int id){
        this.appointmentsId = id;
    }

    public override string ToString()
    {
        return "DeletionRequest [appointmentsId: " + appointmentsId + "]";
    }

}
