using Newtonsoft.Json;
public class ModificationRequest{

    public int appointmentsId { get; set; }
    public int doctorsId { get; set; }
    public DateTime dateTime { get; set; }
    public Emergency emergency { get; set; }

    public ModificationRequest(int id, int doctorsId, DateTime dateTime, Emergency emergency){
        this.appointmentsId = id;
        this.doctorsId = doctorsId;
        this.dateTime = dateTime;
        this.emergency = emergency;
    }

    public override string ToString()
    {
        return "ModificationRequest [appointmentsId: " + appointmentsId + "]";
    }

}
