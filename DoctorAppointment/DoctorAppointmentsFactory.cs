using Newtonsoft.Json;
public class DoctorAppointmentsFactory{
    public List<DoctorAppointment> allDoctorAppointments { get; set; } = null!;

    public DoctorAppointmentsFactory(string path){
        var doctorAppointments = JsonConvert.DeserializeObject<List<DoctorAppointment>>(File.ReadAllText(path));
        if (doctorAppointments != null){
            allDoctorAppointments = doctorAppointments;
        }
    }

    public static void UpdateDoctorAppointments(List<DoctorAppointment> allDoctorAppointments){
        var convertedDoctorAppointments = JsonConvert.SerializeObject(allDoctorAppointments, Formatting.Indented);
        File.WriteAllText("Data/doctorAppointments.json", convertedDoctorAppointments);
    }
}