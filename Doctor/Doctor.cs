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

    public void DeleteAppointment(){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        Console.WriteLine("Enter appointment's id you want to delete: ");
        int id=Convert.ToInt32(Console.ReadLine());
        DoctorAppointment appointmentToDelete=doctorAppointmentsFactory.FindById(id);
        Console.WriteLine(appointmentToDelete);

        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        appointments.GetAllDoctorsAppointments().Remove(appointmentToDelete);
        appointments.UpdateData();

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
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        ReviewTimetable();
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
                        ReviewTimetable();
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
    
    public static void doctorMenu(Doctor doctor){
        Console.WriteLine("Welcome, "+doctor.name+ "!");
        
        Console.WriteLine("\n");

        int option=0;
        DoctorAppointmentsFactory appointments = new DoctorAppointmentsFactory();
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomFactory = new RoomFactory();

        while (option!=4){
            Console.WriteLine("DOCTOR MENU");
            Console.WriteLine("1. CRUD for Doctor Appointments");
            Console.WriteLine("2. Physical examination");
            Console.WriteLine("3. Medicine verification");
            Console.WriteLine("4. Day off request");
            Console.WriteLine("5. Log out");

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
                        appointments.CreateAppointment(doctor);
                        
                    }
                    if (option==2){
                        doctor.ReviewTimetable();
                        Console.WriteLine("Do you want to see someone's medical record? Enter id.");
                        var id=Console.ReadLine();
                        PatientsFactory patients = new PatientsFactory();
                        foreach(Patient patient in patients.GetAllPatients()){
                            if (patient.id==Convert.ToInt32(id)){
                                Console.WriteLine(patient);
                            }
                        }
                        
                    }
                    if (option==3){
                        doctor.UpdateAppointment(doctor);
                    }
                    if (option==4){
                        
                        DoctorsFactory doctorsFactory = new DoctorsFactory();
                        doctorsFactory.GetAllDoctors().Remove(doctor);
                        
                        doctor.DeleteAppointment();

                        doctorsFactory.GetAllDoctors().Add(doctor);
                        doctorsFactory.UpdateData();
                    }
                        
                    }
            }
            if (option==2){
                Console.WriteLine("Physical examination");
                doctor.ReviewTimetable();
                Console.WriteLine("Enter id of the appointment you want to do.");
                int id=Convert.ToInt32(Console.ReadLine());

        
                DoctorAppointment appointmentToDo=null;
                foreach(DoctorAppointment appointment in appointments.GetAllDoctorsAppointments()){
                    if (appointment.id==id){
                        appointmentToDo=appointment;
                        
                    }
                if(appointmentToDo!=null){
                    Console.WriteLine(appointmentToDo.patient);
                    List<DoctorAppointment> permanentAppointments=appointments.GetAllDoctorsAppointments();
                    int number=0;
                    while (number!=5){
                        Console.WriteLine("What do you want to update? Enter number.");
                        Console.WriteLine("1.height\n2.weight\n3.previous illnesses\n4.allergens\n5.done");
                        number=Convert.ToInt32(Console.ReadLine());
                        if(number==1){
                            Console.WriteLine("Enter new patient's height: ");

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory();
                            patients.GetAllPatients().Remove(appointmentToDo.patient);
                            
                            int newHeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.height=newHeight;

                            patients.GetAllPatients().Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            patients.UpdateData();
                            appointments.UpdateData();


                        }
                        else if(number==2){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory();
                            patients.GetAllPatients().Remove(appointmentToDo.patient);
                            
                             Console.WriteLine("Enter new patient's weight: ");
                            int newWeight=Convert.ToInt32(Console.ReadLine());
                            appointmentToDo.patient.medicalRecord.weight=newWeight;

                            patients.GetAllPatients().Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            patients.UpdateData();
                            appointments.UpdateData();

                        }
                        else if(number==3){

                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory();
                            patients.GetAllPatients().Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter patient's previous illnesses: ");
                            string newIllnesses=Console.ReadLine();
                            List<string>illnesses=new List<string>();
                            illnesses.Add(newIllnesses);

                            appointmentToDo.patient.medicalRecord.previousIllnesses=illnesses;

                            patients.GetAllPatients().Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            patients.UpdateData();
                            appointments.UpdateData();
                        }
                        else if(number==4){
                                                       
                            permanentAppointments.Remove(appointmentToDo);
                            PatientsFactory patients = new PatientsFactory();
                            patients.GetAllPatients().Remove(appointmentToDo.patient);
                            
                            Console.WriteLine("Enter new patient's allergens: ");
                            string newAlergens=Console.ReadLine();
                            List<string>alergens=new List<string>();
                            alergens.Add(newAlergens);
                            appointmentToDo.patient.medicalRecord.allergens=alergens;

                            patients.GetAllPatients().Add(appointmentToDo.patient);
                            permanentAppointments.Add(appointmentToDo);
                            patients.UpdateData();
                            appointments.UpdateData();
                        }

                    }
                    Console.WriteLine("Do you want to refer patient to another doctor\n(type yes or no)");
                    string answer=Console.ReadLine();
                    if (answer=="yes"){
                        Console.WriteLine("Enter id of the referral: ");
                        int referralsId=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id of the doctor: ");
                        int doctorsId=Convert.ToInt32(Console.ReadLine());
                        int patientsId=appointmentToDo.patient.id;

                        Referral referral=new Referral(referralsId,patientsId,doctorsId);
                        
                        
                        ReferralsFactory referrals = new ReferralsFactory("Data/referrals.json");
                        referrals.GetAllReferrals().Add(referral);
                        referrals.UpdateData();

                        PatientsFactory patients = new PatientsFactory();
                        patients.GetAllPatients().Remove(appointmentToDo.patient);
                        appointmentToDo.patient.referralsId=referralsId;
                        patients.GetAllPatients().Add(appointmentToDo.patient);
                        patients.UpdateData();

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
                        recipes.GetAllRecipes().Add(recipe);
                        
                        PatientsFactory patients = new PatientsFactory();
                        patients.GetAllPatients().Remove(appointmentToDo.patient);
                        appointmentToDo.patient.recipes.Add(recipeId);
                        patients.GetAllPatients().Add(appointmentToDo.patient);
                        patients.UpdateData();

                        Console.WriteLine("Enter id of the room where physical examination is done: ");
                        int roomId=Convert.ToInt32(Console.ReadLine());

                        RoomFactory rooms = new RoomFactory();
                        Room foundRoom=roomFactory.FindById(roomId);
                        DynamicEquipmentFactory equipment = new DynamicEquipmentFactory();


                        List<DynamicEquipment> roomEquipment=dynamicEquipmentFactory.FindAllEquipmentByIds(foundRoom.equipmentIds);
                        List<int> countedEquipment=dynamicEquipmentFactory.CountTheEquipment();
                        Console.WriteLine("Equipment in room with id ",roomId,":");
                        Console.WriteLine("SterileGauze Hanzaplast Injection Bandage SterileGloves PainKiller:", countedEquipment);

                        Console.WriteLine("Enter equipment that was used for physical examination:");
                        var equipmentType=Console.ReadLine();
                        
                        Console.WriteLine("Enter quantity:");
                        int equipmentQuantity=Convert.ToInt32(Console.ReadLine());

                       //UsedEquipment usedEquipment=new UsedEquipment(equipmentType,equipmentQuantity,roomId);
                        //UsedEquipmentFacotry equipment = new UsedEquipmentFacotry("Data/usedEquipment.json");
                        //equipment.allUsedEquipment.Add(usedEquipment);
                        //UsedEquipmentFacotry.UpdateUsedEquipment(equipment.allUsedEquipment);

                    }
                }
            }
            if (option==3){//NOVO
                Console.WriteLine("Medicine verification");
                MedicinesFactory medicines = new MedicinesFactory();
                foreach (Medicine medicine in medicines.GetAllMedicines()){
                    Console.WriteLine(medicine);
                }
                Console.WriteLine("Enter id of medicine you want to accept or reject: ");
                //int id= Convert.ToInt32(Console.ReadLine());
                //Medicine medicineToVerify=new Medicine();
                foreach (Medicine medicine in medicines.GetAllMedicines()){
                    if(id==medicine.id){
                        medicines.GetAllMedicines().Remove(medicine);
                        //medicineToVerify=medicine;
                        break;
                    }
                }
                
                medicines.UpdateData();
                
                Console.WriteLine("Enter option:\n1. Accept\n2. Reject\n3. Exit");
                int entry= Convert.ToInt32(Console.ReadLine());
                while(entry!=3){
                    Console.WriteLine("Enter option:\n1. Accept\n2. Reject\n3. Exit");
                    int newEntry= Convert.ToInt32(Console.ReadLine());
                    if (newEntry==1){
                        //MedicinesFactory medicines = new MedicinesFactory("Data/medicine.json");
                        //medicines.allMedicines.Add(medicineToVerify);
                        medicines.UpdateData();
                        Console.WriteLine("Medicine accepted successfully.");

                    }else if(newEntry ==2){
                        Console.WriteLine("Add comment: ");
                        var comment=Console.ReadLine();

                        RejectedMedicinesFactory rejectedMedicines = new RejectedMedicinesFactory();
                        //RejectedMedicine rejectedMedicine=new RejectedMedicine(medicineToVerify,comment);
                        //rejectedMedicines.allRejectedMedicines.Add(rejectedMedicine);
                        medicines.UpdateData();
                        

                    }else if(newEntry==3){
                        break;
                    }else{
                        Console.WriteLine("Wrong character entered.");
                    }
                }



            }
            if(option==4){
                Console.WriteLine("Day off request.");
                Console.WriteLine("Enter your first day off: ");
                string dateOfDayOff=Console.ReadLine();
                Console.WriteLine("Enter duration: ");
                int duration=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter cause: ");
                string cause=Console.ReadLine();
                Console.WriteLine("Is it urgent? Type yes or no: ");
                string urgency=Console.ReadLine();
                if(urgency=="yes"){
                    string state="accepted";
                    DayOffRequest dayOffRequest=new DayOffRequest(Convert.ToInt32(DayOffRequestsFactory.FindNextId), doctor.id,dateOfDayOff,duration,state,cause);
                    DayOffRequestsFactory dayOffRequests=new DayOffRequestsFactory();
                    //dayOffRequests.allDayOffRequests.Add(dayOffRequest);
                    //DayOffRequestsFactory.UpdateDayOffRequests(dayOffRequests.allDayOffRequests);

                }else if(urgency=="no"){
                    string state="on hold";
                    DayOffRequest dayOffRequest=new DayOffRequest(Convert.ToInt32(DayOffRequestsFactory.FindNextId), doctor.id,dateOfDayOff,duration,state,cause);
                    DayOffRequestsFactory dayOffRequests=new DayOffRequestsFactory();
                    //dayOffRequests.allDayOffRequests.Add(dayOffRequest);
                    //DayOffRequestsFactory.UpdateDayOffRequests(dayOffRequests.allDayOffRequests);

                }else{
                    Console.WriteLine("Wrong input.");
                }



            }
        }        
    }}
}
