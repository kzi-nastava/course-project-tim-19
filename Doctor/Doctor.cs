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

    public static int FindField(){
        Console.WriteLine("Avaible fields: ");
        Console.WriteLine("1. Surgeon\n2. Cardiologist\n3. Psychiatrist\n 4. Radiologiss\n5. Ophthalmologist\n 6.Paediatrician");
        Console.WriteLine("Enter the option: ");
        var chosenField = Console.ReadLine();
        if (Convert.ToInt32(chosenField) == 1 || Convert.ToInt32(chosenField) == 2 || Convert.ToInt32(chosenField) == 3 || Convert.ToInt32(chosenField) == 4 ||Convert.ToInt32(chosenField) == 5 || Convert.ToInt32(chosenField) == 6){
            return Convert.ToInt32(chosenField)-1;
        }
        return -1;
    }
    
    public void ReviewTimetable(){
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        foreach (int appointmentId in this.doctorAppointments){
            
            foreach (DoctorAppointment appointment in appointments.GetAllDoctorsAppointments()){
                if (appointment.id==appointmentId){
                    Console.WriteLine(appointment);
                }
            }
        }
        
    }

    public bool SetUpAppointment(List<DateTime> availableDates){
        DateTime dateTime=Convert.ToDateTime(Console.ReadLine());

        if(availableDates.Contains(dateTime)){
            Console.WriteLine("This date is not available.");
            return false;
        }
        return true;
    }

    public List<DateTime> CheckAvailability(DateTime dateAndTime){

        String start="2022-04-30T08:00:00";
        DateTime startDate=Convert.ToDateTime(start);

        List<DateTime> available=new List<DateTime>();
        List<DoctorAppointment> appointments=new List<DoctorAppointment>();

        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();

        foreach(int appointment in this.doctorAppointments){
            appointments.Add(doctorAppointmentsFactory.FindById(appointment));
        }

        for (var i=0;i<=2;i++){
            int unavailableAppointment=0;
            while(startDate.Hour!=20){
                DoctorAppointment temp= doctorAppointmentsFactory.FindById(unavailableAppointment);
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

    public static void UpdateAfterExamination(DoctorAppointment appointmentToDo){

        DoctorAppointmentsFactory appointmentsFactory = new DoctorAppointmentsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();

        var foundDoctorAppointment=DoctorAppointmentsFactory.allDoctorAppointments.SingleOrDefault(x=>x.id==appointmentToDo.id);

            if (foundDoctorAppointment!=null){
                DoctorAppointmentsFactory.allDoctorAppointments.Remove(foundDoctorAppointment);
            }
            var foundPatient=PatientsFactory.allPatients.SingleOrDefault(x=>x.id==appointmentToDo.patient.id);

            if (foundPatient!=null){
                PatientsFactory.allPatients.Remove(foundPatient);
            
            PatientsFactory.allPatients.Add(appointmentToDo.patient);
            DoctorAppointmentsFactory.allDoctorAppointments.Add(appointmentToDo);
            patientsFactory.UpdateData();
            appointmentsFactory.UpdateData();
    }
    }

    public void PhysicalExamination(){

        DoctorAppointmentsFactory appointmentsFactory = new DoctorAppointmentsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomFactory = new RoomFactory();

        DoctorAppointment appointmentToDo=null;

                foreach(DoctorAppointment appointment in appointmentsFactory.GetAllDoctorsAppointments()){
                    if (appointment.id==id){
                        appointmentToDo=appointment;
                        
                    }
                if(appointmentToDo!=null){
                    Console.WriteLine(appointmentToDo.patient);
                    int number=0;
                    while (number!=5){
                        Console.WriteLine("\nWhat do you want to update? Enter number.");
                        Console.WriteLine("1.height\n2.weight\n3.previous illnesses\n4.allergens\n5.done");
                        number=Convert.ToInt32(Console.ReadLine());
                        
                        if(number==1){
                            Console.WriteLine("\nEnter new patient's height: ");
                            int newHeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.height=newHeight;

                            Doctor.UpdateAfterExamination(appointmentToDo);

                        }
                        else if(number==2){
                            Console.WriteLine("\nEnter new patient's weight: ");
                            int newWeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.weight=newWeight;

                            Doctor.UpdateAfterExamination(appointmentToDo);


                        }
                        else if(number==3){
                            Console.WriteLine("\nEnter patient's previous illnesses: ");
                            string newIllnesses=Console.ReadLine();
                            List<string>illnesses=new List<string>();
                            illnesses.Add(newIllnesses);

                            Doctor.UpdateAfterExamination(appointmentToDo);
                            
                        }
                        else if(number==4){
                            Console.WriteLine("\nEnter new patient's allergens: ");
                            string newAlergens=Console.ReadLine();
                            List<string>alergens=new List<string>();
                            alergens.Add(newAlergens);
                            appointmentToDo.patient.medicalRecord.allergens=alergens;
                            
                            Doctor.UpdateAfterExamination(appointmentToDo);

                        }

                    }

                    Console.WriteLine("\nDo you want to refer patient to another doctor\n(type yes or no)");
                    string answer=Console.ReadLine();
                    
                    if (answer=="yes"){
                        ReferToAnotherDoctor(appointmentToDo);
                    }
                    
                    Console.WriteLine("\nDo you want to make a recipe?\n(type yes or no)");
                    answer=Console.ReadLine();

                    if (answer=="yes"){
                        MakeARecipe(appointmentToDo);
                    }
                    
                }
            }
        }

    public static void CheckEquipment(){
        RoomFactory roomFactory=new RoomFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory=new DynamicEquipmentFactory();

        Console.WriteLine("\nEnter id of the room where physical examination is done: ");
        int roomId=Convert.ToInt32(Console.ReadLine());
        Room foundRoom=roomFactory.FindById(roomId);
        DynamicEquipmentFactory equipmentFactory = new DynamicEquipmentFactory();

        List<DynamicEquipment> roomEquipment=dynamicEquipmentFactory.FindAllEquipmentByIds(foundRoom.equipmentIds);
        List<int> countedEquipment=dynamicEquipmentFactory.CountTheEquipment(roomEquipment);
        Console.WriteLine("Equipment in room with id ",roomId,":");
        Console.WriteLine("SterileGauze Hanzaplast Injection Bandage SterileGloves PainKiller:", countedEquipment);

        foreach(int equipment in countedEquipment){
            Console.Write(equipment+"           ");
        }
        Console.WriteLine("\nEnter equipment that was used for physical examination:");
        var equipmentType=Console.ReadLine();
        
        Console.WriteLine("\nEnter quantity:");
        int equipmentQuantity=Convert.ToInt32(Console.ReadLine());

        UsedEquipment usedEquipment=new UsedEquipment(equipmentType,equipmentQuantity,roomId);
        UsedEquipmentFactory usedEquipmentFactory = new UsedEquipmentFactory();
        usedEquipmentFactory.GetAllUsedEquipment().Add(usedEquipment);
        usedEquipmentFactory.UpdateData();
    }

    public static void MakeARecipe(DoctorAppointment appointmentToDo){

        PatientsFactory patientsFactory=new PatientsFactory();
        Console.WriteLine("Enter id of recipe: ");
                        int recipeId=Convert.ToInt32(Console.ReadLine());


        Console.WriteLine("Enter id of medicine: ");
        int medicineId=Convert.ToInt32(Console.ReadLine());

        List<int> medicineIds=new List<int>();
        medicineIds.Add(medicineId);
        int moreMedicines=1;

        while(moreMedicines!=2){
            Console.WriteLine("\nDo you want to enter more medicines?\n1.yes\n2.no");
            
            moreMedicines=Convert.ToInt32(Console.ReadLine());
            if(moreMedicines==1){
                Console.WriteLine("Enter id of medicine: ");
                medicineId=Convert.ToInt32(Console.ReadLine());
                medicineIds.Add(medicineId);
            }
        }

        Console.WriteLine("\nHow many times per day patient should use medicines?");
        int timesPerDay=Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nFor how many days patient have to consume medicines?");
        int days=Convert.ToInt32(Console.ReadLine());

        Recipe recipe= new Recipe(recipeId,medicineIds,timesPerDay,days);
            
        RecipesFactory recipesFactory = new RecipesFactory();
        recipesFactory.GetAllRecipes().Add(recipe);
        recipesFactory.UpdateData();
                        
        PatientsFactory patients = new PatientsFactory();
        var foundPatients = patientsFactory.GetAllPatients().SingleOrDefault(x => x.id == appointmentToDo.patient.id);
        if (foundPatients != null) {
            patientsFactory.GetAllPatients().Remove(foundPatients);
        }

        appointmentToDo.patient.recipes.Add(recipeId);
        patients.GetAllPatients().Add(appointmentToDo.patient);
        patients.UpdateData();
    }
    
    public static void ReferToAnotherDoctor(DoctorAppointment appointmentToDo){

        PatientsFactory patientsFactory=new PatientsFactory();
        Console.WriteLine("Enter id of the referral: ");
        int referralsId=Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter id of the doctor: ");
        int doctorsId=Convert.ToInt32(Console.ReadLine());
        int patientsId=appointmentToDo.patient.id;

        Referral referral=new Referral(referralsId,patientsId,doctorsId);
                        
                        
        ReferralsFactory referrals = new ReferralsFactory();
        referrals.GetAllReferrals().Add(referral);
        referrals.UpdateData();

        PatientsFactory patients = new PatientsFactory();
        var foundPatients = patientsFactory.GetAllPatients().SingleOrDefault(x => x.id == patientsId);

        if (foundPatients != null) {
            patientsFactory.GetAllPatients().Remove(foundPatients);
        }

        appointmentToDo.patient.referralsId=referralsId;
        patients.GetAllPatients().Add(appointmentToDo.patient);
        patients.UpdateData();
    }

    public static void CRUD(Doctor doctor){
        int option=0;
                while (option!=5){
                    
                    DoctorAppointmentsFactory appointmentsFactory = new DoctorAppointmentsFactory();
                    PatientsFactory patientsFactory = new PatientsFactory();

                    Console.WriteLine("\nCRUD for Doctor Appointments");
                    Console.WriteLine("1. Create new appointment");
                    Console.WriteLine("2. Timetable review");
                    Console.WriteLine("3. Update an appointment");
                    Console.WriteLine("4. Delete an appointment");
                    Console.WriteLine("5. Back");

                    Console.WriteLine("\nEnter an option: ");
                    option=Convert.ToInt32(Console.ReadLine());

                    if (option==1){
                        appointmentsFactory.CreateAppointment(doctor);
                        
                    }

                    if (option==2){
                        doctor.ReviewTimetable();
                        Console.WriteLine("\nDo you want to see someone's medical record? Enter id.");
                        Console.WriteLine("Type 'no' or id.");
                        var id=Console.ReadLine();
                        if(id=="no"){
                            return;
                        }
                        PatientsFactory patients = new PatientsFactory();
                        foreach(Patient patient in patients.GetAllPatients()){
                            if (patient.id==Convert.ToInt32(id)){
                                Console.WriteLine(patient);
                            }
                        }
                        
                    }

                    if (option==3){
                        doctor.ReviewTimetable();
                        DoctorAppointment appointmentToChange=null;

                        Console.WriteLine("\nEnter id of the appointment you want to change: ");
                        int id=Convert.ToInt32(Console.ReadLine());

                        foreach(DoctorAppointment appointment in appointmentsFactory.GetAllDoctorsAppointments()){
                            if (appointment.id==id){
                                appointmentToChange=appointment;
                            }

                        }

                        DoctorAppointmentsFactory.UpdateAppointment(doctor,appointmentToChange);
                        continue;
                    }

                    if (option==4){
                        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
                        Console.WriteLine("\nEnter appointment's id you want to delete: ");
                        int id=Convert.ToInt32(Console.ReadLine());
                        doctorAppointmentsFactory.Delete(id);
                    }
                        
                    }
    }
    
    public static void doctorMenu(Doctor doctor){
        Console.WriteLine("\nWelcome, "+doctor.name+ "!");
        
        Console.WriteLine("\n");

        int startOption=0;
        DoctorAppointmentsFactory appointmentsFactory = new DoctorAppointmentsFactory();
        PatientsFactory patientsFactory = new PatientsFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomFactory = new RoomFactory();

        while (startOption!=5){

            Console.WriteLine("\nDOCTOR MENU");
            Console.WriteLine("1. CRUD for Doctor Appointments");
            Console.WriteLine("2. Physical examination");
            Console.WriteLine("3. Medicine verification");
            Console.WriteLine("4. Day off request");
            Console.WriteLine("5. Log out");

            Console.WriteLine("\nEnter an option: ");
            startOption=Convert.ToInt32(Console.ReadLine());

            if (startOption==1){
                Doctor.CRUD(doctor);
            }

            if (startOption==2){
                Console.WriteLine("\nPhysical examination");
                doctor.ReviewTimetable();
                Console.WriteLine("\nEnter id of the appointment you want to do.");
                int id=Convert.ToInt32(Console.ReadLine());
                doctor.PhysicalExamination();
        
            }
            
            if (startOption==3){
                Console.WriteLine("\nMedicine verification");
                MedicinesFactory medicineRequestsFactory = new MedicinesFactory("Data/medicineRequests.json");

                foreach (Medicine medicine in medicineRequestsFactory.GetAllMedicines()){
                    Console.WriteLine(medicine);
                }

                Console.WriteLine("Enter id of medicine you want to accept or reject: ");
                int id= Convert.ToInt32(Console.ReadLine());

                Medicine medicineToVerify=null;
                foreach (Medicine medicine in medicineRequestsFactory.GetAllMedicines()){
                    if(id==medicine.id){

                        medicineToVerify=medicine;
                        MedicinesFactory.VerifyMedicine(medicineToVerify);

                    }
                }
            }

            if(startOption==4){
                Console.WriteLine("\nDay off request.");

                Console.WriteLine("\nEnter your first day off: ");
                string dateOfDayOff=Console.ReadLine();
                Console.WriteLine("Enter duration: ");
                int duration=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter cause: ");
                string cause=Console.ReadLine();
                Console.WriteLine("Is it urgent? Type yes or no: ");
                string urgency=Console.ReadLine();
                string state="";

                if(urgency=="yes"){
                    state="accepted";
                    
                }else if(urgency=="no"){
                    state="on hold";
                }
                
                DayOffRequestsFactory dayOffRequestsFactory=new DayOffRequestsFactory();
                DayOffRequest dayOffRequest=new DayOffRequest(Convert.ToInt32(dayOffRequestsFactory.FindNextId()), doctor.id,dateOfDayOff,duration,state,cause);
                DayOffRequestsFactory.CreateDayOffRequest(dayOffRequest);
            }
        }        
        }
}
