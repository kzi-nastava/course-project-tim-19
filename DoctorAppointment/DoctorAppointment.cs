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
            //provera kada je doktor slobodan,prikaz

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

    public static void UpdateAppointment(Doctor doctor){
        int option=0;
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        doctor.ReviewTimetable();
        DoctorAppointment appointmentToChange=null;

        Console.WriteLine("Enter id of the appointment you want to change: ");
        int id=Convert.ToInt32(Console.ReadLine());
        foreach(DoctorAppointment appointment in appointments.GetAllDoctorsAppointments()){
            if (appointment.id==id){
                appointmentToChange=appointment;
            }

        }
        if (appointmentToChange!=null){
            while(option!=3){
                Console.WriteLine("What do you want to update? (Select number)");
                Console.WriteLine("1. Date and time\n2. Emergency\n3. Exit");
                option=Convert.ToInt32(Console.ReadLine());
                if (option==1 ||option==2 ){
                    if(option==1){
                        doctor.ReviewTimetable();
                    }
                    appointments.GetAllDoctorsAppointments().Remove(appointmentToChange);
                    appointmentToChange.UpdateDoctorAppointment(option);
                    appointments.GetAllDoctorsAppointments().Add(appointmentToChange);
                    appointments.UpdateData();

                }
                else if (option!=3){
                    Console.WriteLine("Wrong option entered.");
                }
            }}
        else{
            Console.WriteLine("Wrong id endered.");
        }
    }

}
