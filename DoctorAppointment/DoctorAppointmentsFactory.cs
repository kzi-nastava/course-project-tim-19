using Newtonsoft.Json;
public class DoctorAppointmentsFactory{
    public static List<DoctorAppointment> allDoctorAppointments { get; set; } = null!;

    public DoctorAppointmentsFactory(){
        var doctorAppointments = JsonConvert.DeserializeObject<List<DoctorAppointment>>(File.ReadAllText("Data/doctorAppointments.json"));
        if (doctorAppointments != null){
            allDoctorAppointments = doctorAppointments;
        }
    }

    public List<DoctorAppointment> GetAllDoctorsAppointments(){
        return allDoctorAppointments;
    }

    public void UpdateData(){
        var convertedDoctorAppointments = JsonConvert.SerializeObject(allDoctorAppointments, Formatting.Indented);
        File.WriteAllText("Data/doctorAppointments.json", convertedDoctorAppointments);
    }

    public DoctorAppointment FindById(int id){
        foreach (DoctorAppointment appointment in allDoctorAppointments){
            if (appointment.id == id){
                return appointment;
            }
        }
        return null;
    }

    public int FindNextIdForAppointment(){
        List<int> allIds = new List<int>();
        foreach (DoctorAppointment appointment in allDoctorAppointments){
            allIds.Add(appointment.id);
        }
        for (int i = 0; i < 100; ++i){
            if (!allIds.Contains(i)){
                return i;
            }
        }
        return 0;
    }

    public void Delete(int appointmentsId){ 
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        DoctorAppointment appointment = doctorAppointmentsFactory.FindById(appointmentsId);
        allDoctorAppointments.Remove(appointment);
        doctorAppointmentsFactory.UpdateData();
        try {
            Doctor doctor = doctorsFactory.FindById(appointment.doctorsId);
            var foundDoctor = doctorsFactory.GetAllDoctors().SingleOrDefault(x => x.id == doctor.id);
            if (foundDoctor != null) {
                doctorsFactory.GetAllDoctors().Remove(foundDoctor);
            }
            doctor.doctorAppointments.Remove(appointmentsId);
            doctorsFactory.GetAllDoctors().Add(doctor);
            doctorsFactory.UpdateData();
        } catch (NullReferenceException e) {

        }
    }

     public void Update(ModificationRequest request){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorAppointment appointment = doctorAppointmentsFactory.FindById(request.appointmentsId);
        var foundAppointment = allDoctorAppointments.SingleOrDefault(x => x.id == appointment.id);
            if (foundAppointment != null) {
                allDoctorAppointments.Remove(foundAppointment);
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
        allDoctorAppointments.Add(appointment);
        doctorAppointmentsFactory.UpdateData();
    }

    
    public void CreateViaReferral(int referralsId, List<Referral> allReferrals, int patientsId, int doctorsId, List<Patient> allPatients, List<Doctor> allDoctors, List<DoctorAppointment> allAppointments) {
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();
        ReferralsFactory referralsFactory = new ReferralsFactory();
        Console.WriteLine("Enter appointment's id: ");
        var appointmentsId = Console.ReadLine();
        Patient patient = patientsFactory.FindById(patientsId);
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
        if (!doctorAppointmentsFactory.CheckAvailability(doctorsId, newDateTime)){
            DoctorAppointment newAppointment = new DoctorAppointment(Convert.ToInt32(appointmentsId), doctorsId, patient, newDateTime, (Emergency)1);
            allAppointments.Add(newAppointment);
            doctorAppointmentsFactory.UpdateData();
            Doctor doctor = doctorsFactory.FindById(doctorsId);
            var foundDoctor = allDoctors.SingleOrDefault(x => x.id == doctor.id);
            if (foundDoctor != null) {
                allDoctors.Remove(foundDoctor);
            }
            doctor.doctorAppointments.Add(Convert.ToInt32(appointmentsId));
            allDoctors.Add(doctor);
            doctorsFactory.UpdateData();
            Referral referral = referralsFactory.FindById(referralsId);
            var foundReferral = allReferrals.SingleOrDefault(x => x.referralsId == referral.referralsId);
            if (foundReferral != null) {
                allReferrals.Remove(foundReferral);
            }
            referralsFactory.UpdateData();
            Console.WriteLine("You successfully created a new appointment!");
        } else {
            Console.WriteLine("Doctor is not avaible at that time.");
        }
    }
    
    public bool CheckAvailability(int doctorsId, DateTime date){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        Doctor doctor = doctorsFactory.FindById(doctorsId);
        List<DateTime> dates = new List<DateTime>();
        foreach(int i in doctor.doctorAppointments){
            try{
                DoctorAppointment appointment = doctorAppointmentsFactory.FindById(i);
                dates.Add(appointment.dateTime);
            } catch (NullReferenceException e) {

            }
        }
        return dates.Contains(date);
    }
    
    public void CreateUrgentAppointment(int patientsId, Field doctorsField, List<Patient> allPatients, List<Doctor> allDoctors){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();
        Doctor foundDoctor = doctorsFactory.FindByField(doctorsField);
        List<DateTime> firstAvaibleAppointments = FindAvaibleAppointment(foundDoctor);
        Patient patient = patientsFactory.FindById(patientsId);
        if (firstAvaibleAppointments.Count == 1){
            DoctorAppointment urgentAppointment = new DoctorAppointment(doctorAppointmentsFactory.FindNextIdForAppointment(), foundDoctor.id, patient, firstAvaibleAppointments[0], (Emergency)0);
        } else {
            Console.WriteLine("There's no avaible time in the next two hours.");
            Console.WriteLine("Here are the first five appointments that can be rescheduled: ");
            for (int i = 0; i < firstAvaibleAppointments.Count; i++){
                Console.WriteLine(i+1 + "." + firstAvaibleAppointments[i].ToString());
            }
            Console.WriteLine("Enter the option: ");
            var chosenDate = Console.ReadLine();
            DoctorAppointment appointmentToReschedule = DoctorAppointment.FindByDoctorAndDate(foundDoctor, firstAvaibleAppointments[Convert.ToInt32(chosenDate)]);
            var appointmentToRemove = allDoctorAppointments.SingleOrDefault(x => x.id == appointmentToReschedule.id);
            if (appointmentToRemove != null) {
                allDoctorAppointments.Remove(appointmentToRemove);
            }
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
            if (!doctorAppointmentsFactory.CheckAvailability(foundDoctor.id, newDateTime)){
                DoctorAppointment rescheduledAppointment = new DoctorAppointment(appointmentToReschedule.id, appointmentToReschedule.doctorsId, appointmentToReschedule.patient, newDateTime, appointmentToReschedule.emergency);
            }
            doctorAppointmentsFactory.UpdateData();
        }
    }

    
    public static List<DateTime> FindAvaibleAppointment(Doctor foundDoctor){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        List<DateTime> unavaibleDates = new List<DateTime>();
        List<DateTime> avaibleDates = new List<DateTime>();
        foreach (int appointmentId in foundDoctor.doctorAppointments){
                try {
                    DoctorAppointment appointment = doctorAppointmentsFactory.FindById(appointmentId);
                    unavaibleDates.Add(appointment.dateTime);
                } catch (NullReferenceException e) {

                }
            }
        for (int i = 0; i < 2; i ++) {
            int j = 0;
            while (j < 60){
                DateTime now = new DateTime(2022, 5, 14, 12 + i, j, 0); //change to current time
                avaibleDates.Add(now);
                j += 15;
            }
        }
        foreach (DateTime date in unavaibleDates) {
            if (avaibleDates.Contains(date)){
                avaibleDates.Remove(date);
            }
        }
        List<DateTime> finalDateTime = new List<DateTime>();
        if (avaibleDates.Count != 0){
            finalDateTime.Add(avaibleDates[0]);
        } else {
            for (int i = 0; i < 5; i++){
                finalDateTime.Add(unavaibleDates[i]);
            }
        }
        return finalDateTime;
    }

    public void CreateAppointment(Doctor doctor){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Create new appointment");
        Console.WriteLine("Enter id: ");
        var id=Console.ReadLine();
        if (doctorAppointmentsFactory.FindById(Convert.ToInt32(id))==null){
            Console.WriteLine("Enter patient's id: ");
            var patientsId=Console.ReadLine();
            Patient patient = patientsFactory.FindById(Convert.ToInt32(patientsId));
            if(patient!=null){
                Console.WriteLine("Enter date and time: ");
                var dateTime=Console.ReadLine();
                List<DateTime> availableDates=doctor.CheckAvailability(Convert.ToDateTime(dateTime));
                Console.WriteLine("Enter emergency (0 for urgent, 1 for not-urgent): ");
                var emergency= Convert.ToInt32(Console.ReadLine());

                if(emergency==0 ||emergency==1){
                    DoctorAppointment newAppointment=new DoctorAppointment(Convert.ToInt32(id), doctor.id, patient,  Convert.ToDateTime(dateTime), (Emergency)emergency);
                
                    DoctorsFactory doctors = new DoctorsFactory();
                    doctors.GetAllDoctors().Remove(doctor);
                    DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
                    appointments.GetAllDoctorsAppointments().Add(newAppointment);
                    appointments.UpdateData();

                    doctor.doctorAppointments.Add(Convert.ToInt32(id));
                    doctors.GetAllDoctors().Add(doctor);
                    doctors.UpdateData();
                    
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

}