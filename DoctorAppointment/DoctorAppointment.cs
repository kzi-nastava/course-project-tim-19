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
    
    public static DoctorAppointment FindById(int id){
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        foreach (DoctorAppointment appointment in appointments.allDoctorAppointments){
            if (appointment.id == id){
                return appointment;
            }
        }
        return null;
    }

    public static void UpdateData(List<DoctorAppointment> allAppointments){
        var convertedAppointments = JsonConvert.SerializeObject(allAppointments, Formatting.Indented);
        File.WriteAllText("Data/doctorAppointments.json", convertedAppointments);
    }
    public static void Delete(int appointmentsId, List<DoctorAppointment> allAppointments, List<Doctor> allDoctors){
        DoctorAppointment appointment = FindById(appointmentsId);
        allAppointments.Remove(appointment);
        UpdateData(allAppointments);
        Doctor doctor = Doctor.FindById(appointment.doctorsId, allDoctors);
        allDoctors.Remove(doctor);
        doctor.doctorAppointments.Remove(appointment);
        allDoctors.Add(doctor);
        Doctor.UpdateData(allDoctors);
    }

    public static void Update(ModificationRequest request, List<DoctorAppointment> allAppointments, List<Doctor> allDoctors){
        DoctorAppointment appointment = FindById(request.appointmentsId);
        allAppointments.Remove(appointment);
        if (request.doctorsId != 0){
            appointment.doctorsId = request.doctorsId;
        }
        DateTime date = new DateTime();
        if (request.dateTime != date){
            appointment.dateTime = request.dateTime;
        }
        if (request.emergency != (Emergency)2){
            appointment.emergency = request.emergency;
        }
        allAppointments.Add(appointment);
        UpdateData(allAppointments);
        Doctor doctor = Doctor.FindById(appointment.doctorsId, allDoctors);
        allDoctors.Remove(doctor);
        doctor.doctorAppointments.Remove(appointment);
        doctor.doctorAppointments.Add(appointment);
        allDoctors.Add(doctor);
        Doctor.UpdateData(allDoctors);
    }

}
