using Newtonsoft.Json;

public enum Emergency{
    Urgent,
    NotUrgent
}
public class DoctorAppointment
{
    public int id { get; set; }
    public int doctorsId { get; set; }
    public Patient patient { get; set; }
    public DateTime dateTime { get; set; }
    public Emergency emergency{ get; set; }

    public DoctorAppointment(int id, int doctorsId, Patient patient,  DateTime dateTime, Emergency emergency){
        this.id = id;
        this.doctorsId = doctorsId;
        this.patient = patient;
        this.dateTime = dateTime;
        this.emergency = emergency;
    }

    public override string ToString()
    {
        return "DoctorAppointment [id: " + id + ", doctor's id: " + doctorsId + ", patient:" + patient + ", date and time: " + dateTime + ", emergency: " + emergency + "]";
    }
}