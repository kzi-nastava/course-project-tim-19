public class Secretary{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public Secretary(int id, string name, string email, string password){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
    }

    public override string ToString()
    {
        return "Secretary [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + "]";
    }
    
    public static Secretary FindById(int id, List<Secretary> allSecretaries){
        foreach (var secretary in allSecretaries){
            if (secretary.id == id){
                return secretary;
            }
        }
        return null;
    }
    public static void Menu(Secretary secretary){
        PatientsFactory patientsFactory = new PatientsFactory();
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DeletionRequestsFactory deletionRequestsFactory = new DeletionRequestsFactory();
        ModificationRequestsFactory modificationRequestsFactory = new ModificationRequestsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        ReferralsFactory referralsFectory = new ReferralsFactory("Data/referrals.json");
        DynamicEquipmentFactory dynamicEquipmentFactory = new DynamicEquipmentFactory();
        RoomFactory roomFactory = new RoomFactory();
        ReviewedDaysOffFactory reviewedDaysOffFactory = new ReviewedDaysOffFactory();
        Console.WriteLine("Welcome, " + secretary.name + "!");
        Console.WriteLine("MENU FOR SECRETARY");
        while (true){
            Console.WriteLine("1. Patient accounts management\n2. Requests for modification and deletion of doctor appointments management\n3. Create a doctors appointment for patient via their referral\n4. Create urgent appointment\n5. Create request for missing equipment\n6. Rearrange dynamic equipment\n7. Manage requests for day off\n8. Log out");
            Console.WriteLine("Enter the option: ");
            var optionForMainMenu = Console.ReadLine();
            if (optionForMainMenu == "1"){
                while (true){
                    Console.WriteLine("1. Create new patient account\n2. Modify patient account\n3. Delete patient account\n4. View all patients\n5. Block patient account\n6. Unblock patient account\n7. View all blocked patients\n8. Back");
                    Console.WriteLine("Enter the option: ");
                    var optionForPatientAccountsMenu = Console.ReadLine();
                    if (optionForPatientAccountsMenu == "1"){
                        patientsFactory.CreateNewAccount();
                    } else if (optionForPatientAccountsMenu == "2"){
                        patientsFactory.ModifyAccount();
                    } else if (optionForPatientAccountsMenu == "3"){
                        patientsFactory.DeleteAccount();
                    } else if (optionForPatientAccountsMenu == "4"){
                        patientsFactory.ViewAllPatients();
                    } else if (optionForPatientAccountsMenu == "5"){
                        patientsFactory.BlockAccount();
                    } else if (optionForPatientAccountsMenu == "6"){
                        patientsFactory.UnblockAccount();
                    } else if (optionForPatientAccountsMenu == "7"){
                        patientsFactory.ViewAllBlockedPatients();
                    } else if (optionForPatientAccountsMenu == "8"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "2"){
                while (true) {
                    Console.WriteLine("1. View modification requests\n2. View deletion requests\n3. Back");
                    Console.WriteLine("Enter the option:");
                    var optionForRequestsMenu = Console.ReadLine();
                    if (optionForRequestsMenu == "1"){
                        modificationRequestsFactory.ManageRequest();
                    } else if (optionForRequestsMenu == "2"){
                       deletionRequestsFactory.ManageDeletionRequests();
                    } else if (optionForRequestsMenu == "3"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "3"){
                while (true) {
                    Console.WriteLine("1. View all patients with referrals\n2. Create appointment for patient\n3. Back");
                    Console.WriteLine("Enter the option:");
                    var optionForReferralsMenu = Console.ReadLine();
                    if (optionForReferralsMenu == "1"){
                        patientsFactory.ViewAllPatientsWithReferral();
                    } else if (optionForReferralsMenu == "2"){
                        Referral referral = Referral.FindReferralByPatient(patientsFactory.GetAllPatients());
                        doctorAppointmentsFactory.CreateViaReferral(referral.referralsId, referralsFectory.GetAllReferrals(), referral.patientsId, referral.doctorsId, patientsFactory.GetAllPatients(), doctorsFactory.GetAllDoctors(), doctorAppointmentsFactory.GetAllDoctorsAppointments());
                    } else if (optionForReferralsMenu == "3"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong option! Please try again.");
                    }
                }
            } else if (optionForMainMenu == "4"){
                Console.WriteLine("Enter patient's id: ");
                var patientsId = Console.ReadLine();
                int field = Doctor.FindField();
                doctorAppointmentsFactory.CreateUrgentAppointment(Convert.ToInt32(patientsId), (Field)field, patientsFactory.GetAllPatients(), doctorsFactory.GetAllDoctors());
            } else if (optionForMainMenu == "5"){
                dynamicEquipmentFactory.OrderMissingEquipment(roomFactory.GetAllRooms());
            } else if (optionForMainMenu == "6"){
                DynamicEquipment.RearrangeTheEquipment(roomFactory.GetAllRooms(), dynamicEquipmentFactory.GetAllDynamicEquipment());
            } else if (optionForMainMenu == "7"){
                reviewedDaysOffFactory.ReviewDayOffRequests();
            } else if (optionForMainMenu == "8"){
                break;
            } else {
                Console.WriteLine("You entered wrong option! Please try again.");
            }
        }
    }

}
