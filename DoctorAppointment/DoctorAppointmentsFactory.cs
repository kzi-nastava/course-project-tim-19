using Newtonsoft.Json;
public class DoctorAppointmentsFactory{
    public List<DoctorAppointment> allDoctorAppointments { get; set; } = null!;

    public DoctorAppointmentsFactory(string path){
        var doctorAppointments = JsonConvert.DeserializeObject<List<DoctorAppointment>>(File.ReadAllText(path));
        if (doctorAppointments != null){
            allDoctorAppointments = doctorAppointments;
        }
    }
}