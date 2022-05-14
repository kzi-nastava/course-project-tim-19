using Newtonsoft.Json;

public enum Field{
    Surgeon,
    Kardiolog,
    Psychiatrists,
    Radiologists,
    Ophthalmology,
    Paediatricians,
} 

public class Doctor
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public Field field{ get; set; }
    public List<int> doctorAppointments { get; set; }

    public Doctor(int id, string name, string email, string password, Field field, List<int> doctorAppointments){
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
    
    public static Doctor FindById(int id, List<Doctor> allDoctors){
        foreach (Doctor doctor in allDoctors){
            if (doctor.id == id){
                return doctor;
            }
        }
        return null;
    }

    public static void UpdateData(List<Doctor> allDoctors){
        var convertedDoctors = JsonConvert.SerializeObject(allDoctors, Formatting.Indented);
        File.WriteAllText("Data/doctors.json", convertedDoctors);
    }
    
    public void ReviewTimetable(){
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        foreach (int appointmentId in this.doctorAppointments){
            
            foreach (DoctorAppointment appointment in appointments.allDoctorAppointments){
                if (appointment.id==appointmentId){
                    Console.WriteLine(appointment);
                }
            }
        }
        
    }

    public static Doctor FindDoctorById(int id){
        
        DoctorsFactory doctors = new DoctorsFactory("Data/doctors.json");
        foreach (Doctor doctor in doctors.allDoctors){
            if (doctor.id == id){
                return doctor;
            }
        }
        return null;
    }

    public DoctorAppointment FindDoctorsAppointmentById(int id){
        
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        foreach (DoctorAppointment doctorsAppointment in appointments.allDoctorAppointments){
            if (doctorsAppointment.id == id){
                return doctorsAppointment;
            }
        }
        return null;
    }

    public void DeleteAppointment(){
        Console.WriteLine("Enter appointment's id you want to delete: ");
        int id=Convert.ToInt32(Console.ReadLine());
        DoctorAppointment appointmentToDelete=DoctorAppointment.FindById(id);
        Console.WriteLine(appointmentToDelete);

        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        appointments.allDoctorAppointments.Remove(appointmentToDelete);
        DoctorAppointmentsFactory.UpdateDoctorAppointments(appointments.allDoctorAppointments);

        this.doctorAppointments.Remove(id);
    }

    public bool SetUpAppointment(List<DateTime> availableDates){
        DateTime dateTime=Convert.ToDateTime(Console.ReadLine());
        if(availableDates.Contains(dateTime)){
            Console.WriteLine("This date is not available.");
            return false;
        }
        return true;
    }

    public void UpdateAppointment(Doctor doctor){
        int option=0;
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        ReviewTimetable();
        DoctorAppointment appointmentToChange=null;

        Console.WriteLine("Enter id of the appointment you want to change: ");
        int id=Convert.ToInt32(Console.ReadLine());
        foreach(DoctorAppointment appointment in appointments.allDoctorAppointments){
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
                        ReviewTimetable();
                    }
                    appointments.allDoctorAppointments.Remove(appointmentToChange);
                    appointmentToChange.UpdateDoctorAppointment(option);
                    appointments.allDoctorAppointments.Add(appointmentToChange);
                    DoctorAppointmentsFactory.UpdateDoctorAppointments(appointments.allDoctorAppointments);

                }
                else if (option!=3){
                    Console.WriteLine("Wrong option entered.");
                }
            }}
        else{
            Console.WriteLine("Wrong id endered.");
        }
    }

    public List<DateTime> CheckAvailability(DateTime dateAndTime){
        String start="2022-04-30T08:00:00";
        DateTime startDate=Convert.ToDateTime(start);

        List<DateTime> available=new List<DateTime>();
        List<DoctorAppointment> appointments=new List<DoctorAppointment>();

        foreach(int appointment in this.doctorAppointments){
            appointments.Add(FindDoctorsAppointmentById(appointment));
        }

        for (var i=0;i<=2;i++){
            int unavailableAppointment=0;
            while(startDate.Hour!=20){
                DoctorAppointment temp= FindDoctorsAppointmentById(unavailableAppointment);
                if(Convert.ToDateTime(startDate)!=available[0]){
                    available.Add(startDate);
                    Console.WriteLine(Convert.ToString(startDate.Hour),Convert.ToString(startDate.Minute));
                }
                else{
                    Console.WriteLine("Unavailable date.");
                }
            }

            startDate.AddDays(1);
        }
        return available;
    }
    
    public static void doctorMenu(Doctor doctor){
        Console.WriteLine("Welcome, "+doctor.name+ "!");
        
        Console.WriteLine("\n");

        int option=0;
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory("Data/doctorAppointments.json");

        while (option!=3){
            Console.WriteLine("DOCTOR MENU");
            Console.WriteLine("1. CRUD for Doctor Appointments");
            Console.WriteLine("2. Physical examination");
            Console.WriteLine("3. Log out");

            Console.WriteLine("Enter an option: ");
            option=Convert.ToInt32(Console.ReadLine());
            if (option==1){
                while (option!=5){
                    Console.WriteLine("CRUD for Doctor Appointments");
                    Console.WriteLine("1. Create new appointment");
                    Console.WriteLine("2. Timetable review");
                    Console.WriteLine("3. Update an appointment");
                    Console.WriteLine("4. Delete an appointment");
                    Console.WriteLine("5. Back");

                    Console.WriteLine("Enter an option: ");
                    option=Convert.ToInt32(Console.ReadLine());
                    if (option==1){
                        DoctorAppointment.CreateAppointment(doctor);
                        
                    }
                    if (option==2){
                        doctor.ReviewTimetable();
                        Console.WriteLine("Do you want to see someone's medical record? Enter id.");
                        var id=Console.ReadLine();
                        PatientsFactory patients = new PatientsFactory("Data/patients.json");
                        foreach(Patient patient in patients.allPatients){
                            if (patient.id==Convert.ToInt32(id)){
                                Console.WriteLine(patient);
                            }
                        }
                        
                    }
                    if (option==3){
                        doctor.UpdateAppointment(doctor);
                    }
                    if (option==4){
                        
                        DoctorsFactory doctors = new DoctorsFactory("Data/doctors.json");
                        doctors.allDoctors.Remove(doctor);
                        
                        doctor.DeleteAppointment();

                        doctors.allDoctors.Add(doctor);
                        DoctorsFactory.UpdateDoctors(doctors.allDoctors);
                    }
                        
                    }
            }
            if (option==2){
                Console.WriteLine("Physical examination");
                doctor.ReviewTimetable();
                Console.WriteLine("Enter id of the appointment you want to do.");
                int id=Convert.ToInt32(Console.ReadLine());

        
                DoctorAppointment appointmentToDo=null;
                foreach(DoctorAppointment appointment in appointments.allDoctorAppointments){
                    if (appointment.id==id){
                        appointmentToDo=appointment;
                        
                    }
                if(appointmentToDo!=null){
                    Console.WriteLine(appointmentToDo.patient);
                    List<DoctorAppointment> permanentAppointments=appointments.allDoctorAppointments;
                    int number=0;
                    while (number!=5){
                        Console.WriteLine("What do you want to update? Enter number.");
                        Console.WriteLine("1.height\n2.weight\n3.previous illnesses\n4.allergens\n5.done");
                        number=Convert.ToInt32(Console.ReadLine());
                        if(number==1){
                            Console.WriteLine("Enter new patient's height: ");

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            int newHeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.height=newHeight;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);


                        }
                        else if(number==2){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                             Console.WriteLine("Enter new patient's weight: ");
                            int newWeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.weight=newWeight;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);

                        }
                        else if(number==3){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter patient's previous illnesses: ");
                            string newIllnesses=Console.ReadLine();
                            List<string>illnesses=new List<string>();
                            illnesses.Add(newIllnesses);

                            appointmentToDo.patient.medicalRecord.previousIllnesses=illnesses;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);
                        }
                        else if(number==4){
                                                       
                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory("Data/patients.json");
                            patients.allPatients.Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter new patient's allergens: ");
                            string newAlergens=Console.ReadLine();
                            List<string>alergens=new List<string>();
                            alergens.Add(newAlergens);
                            appointmentToDo.patient.medicalRecord.allergens=alergens;

                            patients.allPatients.Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            PatientsFactory.UpdatePatients(patients.allPatients);
                            DoctorAppointmentsFactory.UpdateDoctorAppointments(permanentAppointments);
                        }

                    }
                
            
                    Console.WriteLine("Do you want to make a recipe?\n(type yes or no)"); //NOVO
                    answer=Console.ReadLine();
                    if (answer=="yes"){
                        Console.WriteLine("Enter id of recipe: "); //NOVO
                        int recipeId=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id of medicine: ");
                        int medicineId=Convert.ToInt32(Console.ReadLine());
                        List<int> medicineIds=new List<int>();
                        medicineIds.Add(medicineId);
                        int moreMedicines=1;
                        while(option!=2){
                            Console.WriteLine("Do you want to enter more medicines?\n1.yes\n2.no");
                            
                            moreMedicines=Convert.ToInt32(Console.ReadLine());
                            if(moreMedicines==1){
                                Console.WriteLine("Enter id of medicine: ");
                                medicineId=Convert.ToInt32(Console.ReadLine());
                                medicineIds.Add(medicineId);
                            }
                        }
                        Console.WriteLine("How many times per day patient should use medicines?");
                        int timesPerDay=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("For how many days patient have to consume medicines?");
                        int days=Convert.ToInt32(Console.ReadLine());
                        Recipe recipe= new Recipe(recipeId,medicineIds,timesPerDay,days);
                        
                        RecipesFactory recipes = new RecipesFactory("Data/recipes.json");
                        recipes.allRecipes.Add(recipe);
                        
                        PatientsFactory patients = new PatientsFactory("Data/patients.json");
                        patients.allPatients.Remove(appointmentToDo.patient);
                        appointmentToDo.patient.recipes.Add(recipeId);
                        patients.allPatients.Add(appointmentToDo.patient);
                        PatientsFactory.UpdatePatients(patients.allPatients);
                    }}
                    
            
        }       
    }}}
}
