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

    public static DoctorAppointment FindByDoctorAndDate(Doctor doctor, DateTime date){
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        foreach (DoctorAppointment appointment in appointments.GetAllDoctorsAppointments()){
            if (appointment.doctorsId == doctor.id && appointment.dateTime == date){
                return appointment;
            }
        }
        return null;
    }

    public void UpdateDoctorAppointment(int option){
        if(option==1){

            Console.WriteLine("Type available date and time.");
            DateTime newDateAndTime=Convert.ToDateTime(Console.ReadLine());

            this.dateTime=newDateAndTime;

        }
        else if(option==2){
            Console.WriteLine("Enter emergency (0 for urgent, 1 for not-urgent): ");
            var emergency= Convert.ToInt32(Console.ReadLine());
            if(emergency==0 ||emergency==1){
                this.emergency=(Emergency)emergency;                 
            }
            else{
                Console.WriteLine("Incorrect input. Enter 0 for urgent or 1 for not-urgent.");
            }
            
        }

    }

}
