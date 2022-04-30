using Newtonsoft.Json;

public enum Field{
    Surgeon,
    Kardiolog
}

public class Doctor
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public Field field{ get; set; }
    public List<DoctorAppointment> doctorAppointments { get; set; }

    public Doctor(int id, string name, string email, string password, Field field, List<DoctorAppointment> doctorAppointments){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.field = field;
        this.doctorAppointments = doctorAppointments;
    }

    public override string ToString()
    {
        string appointments = "";
        foreach (var appointment in doctorAppointments){
            appointments += appointment.ToString() + " ";
        }
        return "Doctor [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + ", field: " + field + ", doctor appointment: [" + appointments + "]]";
    }
}