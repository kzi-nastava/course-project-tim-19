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
        try {
            Doctor doctor = Doctor.FindById(appointment.doctorsId, allDoctors);
            var foundDoctor = allDoctors.SingleOrDefault(x => x.id == doctor.id);
            if (foundDoctor != null) {
                allDoctors.Remove(foundDoctor);
            }
            doctor.doctorAppointments.Remove(appointmentsId);
            allDoctors.Add(doctor);
            Doctor.UpdateData(allDoctors);
        } catch (NullReferenceException e) {

        }
    }

    public static void Update(ModificationRequest request, List<DoctorAppointment> allAppointments, List<Doctor> allDoctors){
        DoctorAppointment appointment = FindById(request.appointmentsId);
        var foundAppointment = allAppointments.SingleOrDefault(x => x.id == appointment.id);
            if (foundAppointment != null) {
                allAppointments.Remove(foundAppointment);
            }
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
    }

    
    public static void CreateViaReferral(int referralsId, List<Referral> allReferrals, int patientsId, int doctorsId, List<Patient> allPatients, List<Doctor> allDoctors, List<DoctorAppointment> allAppointments) {
        Console.WriteLine("Enter appointment's id: ");
        var appointmentsId = Console.ReadLine();
        Patient patient = Patient.FindById(patientsId, allPatients);
        Console.WriteLine("Enter the date.\nYear: ");
        var year = Console.ReadLine();
        Console.WriteLine("Month: ");
        var month = Console.ReadLine();
        Console.WriteLine("Day: ");
        var day = Console.ReadLine();
        Console.WriteLine("Hour: ");
        var hour = Console.ReadLine();
        Console.WriteLine("Minutes: ");
        var minutes = Console.ReadLine();
        var newDateTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(minutes), 00);
        if (!CheckAvailability(doctorsId, allDoctors, newDateTime)){
            DoctorAppointment newAppointment = new DoctorAppointment(Convert.ToInt32(appointmentsId), doctorsId, patient, newDateTime, (Emergency)1);
            allAppointments.Add(newAppointment);
            UpdateData(allAppointments);
            Doctor doctor = Doctor.FindById(doctorsId, allDoctors);
            var foundDoctor = allDoctors.SingleOrDefault(x => x.id == doctor.id);
            if (foundDoctor != null) {
                allDoctors.Remove(foundDoctor);
            }
            doctor.doctorAppointments.Add(Convert.ToInt32(appointmentsId));
            allDoctors.Add(doctor);
            Doctor.UpdateData(allDoctors);
            Referral referral = Referral.FindById(referralsId);
            var foundReferral = allReferrals.SingleOrDefault(x => x.referralsId == referral.referralsId);
            if (foundReferral != null) {
                allReferrals.Remove(foundReferral);
            }
            Referral.UpdateData(allReferrals);
            Console.WriteLine("You successfully created a new appointment!");
        } else {
            Console.WriteLine("Doctor is not avaible at that time.");
        }
    }
    
    public static bool CheckAvailability(int doctorsId, List<Doctor> allDoctors, DateTime date){
        Doctor doctor = Doctor.FindById(doctorsId, allDoctors);
        List<DateTime> dates = new List<DateTime>();
        foreach(int i in doctor.doctorAppointments){
            try{
                DoctorAppointment appointment = DoctorAppointment.FindById(i);
                dates.Add(appointment.dateTime);
            } catch (NullReferenceException e) {

            }
        }
        return dates.Contains(date);
    }
    
    public static void CreateAppointment(Doctor doctor){

        Console.WriteLine("Create new appointment");
        Console.WriteLine("Enter id: ");
        var id=Console.ReadLine();
        if (FindById(Convert.ToInt32(id))==null){
            Console.WriteLine("Enter patient's id: ");
            var patientsId=Console.ReadLine();
            Patient patient =Patient.FindPatientById(Convert.ToInt32(patientsId));
            if(patient!=null){
                Console.WriteLine("Enter date and time: ");
                var dateTime=Console.ReadLine();
                //List<DateTime> availableDates=doctor.CheckAvailability(Convert.ToDateTime(dateTime));
                Console.WriteLine("Enter emergency (0 for urgent, 1 for not-urgent): ");
                var emergency= Convert.ToInt32(Console.ReadLine());

                if(emergency==0 ||emergency==1){
                    DoctorAppointment newAppointment=new DoctorAppointment(Convert.ToInt32(id), patient,  Convert.ToDateTime(dateTime), (Emergency)emergency);
                
                    DoctorsFactory doctors = new DoctorsFactory("Data/doctors.json");
                    doctors.allDoctors.Remove(doctor);
                    DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
                    appointments.allDoctorAppointments.Add(newAppointment);
                    DoctorAppointmentsFactory.UpdateDoctorAppointments(appointments.allDoctorAppointments);

                    doctor.doctorAppointments.Add(Convert.ToInt32(id));
                    doctors.allDoctors.Add(doctor);
                    DoctorsFactory.UpdateDoctors(doctors.allDoctors);
                    
                    //doctor.setUpAppointment(AvailableDates);                 
                }
                else{
                    Console.WriteLine("Incorrect input. Enter 0 for urgent or 1 for not-urgent.");
                }
                
            }
            else{
                Console.WriteLine("Patient with this id does not exist.");
            }
        }
        else{
            Console.WriteLine("Id already exists.");
        }
        
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

}
