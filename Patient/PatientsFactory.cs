using Newtonsoft.Json;
public class PatientsFactory{
    public static List<Patient> allPatients { get; set; } = null!;

    public PatientsFactory(){
        var patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("Data/patients.json"));
        if (patients != null){
            allPatients = patients;
        }
    }

    public void UpdateData(){
        var convertedPatients = JsonConvert.SerializeObject(allPatients, Formatting.Indented);
        File.WriteAllText("Data/patients.json", convertedPatients);
    }

    public List<Patient> GetAllPatients(){
        return allPatients;
    }

    public Patient FindById(int id){
        foreach (Patient patient in allPatients){
            if (patient.id == id){
                return patient;
            }
        }
        return null;
    }

    public static List<string> EnterIllnesses(){
        List<string> previousIllnesses = new List<string>();
        while (true){
            Console.WriteLine("Does patient have any previous illnesses? (enter 'yes' or 'no')");
            var answerForIllnesses = Console.ReadLine();
            if (answerForIllnesses == "yes"){
                Console.WriteLine("Enter illness:");
                var illness = Console.ReadLine();
                while (illness == ""){
                    Console.WriteLine("Enter illness:");
                    illness = Console.ReadLine();
                }
                if (illness != null){
                    previousIllnesses.Add(illness);
                }
                while (true){
                    Console.WriteLine("Is there more? (enter 'yes' or 'no')");
                    var answerForMoreIllnesses = Console.ReadLine();
                    if (answerForMoreIllnesses == "yes"){
                        Console.WriteLine("Enter illness:");
                        var anotherIllness = Console.ReadLine();
                        while (anotherIllness == ""){
                            Console.WriteLine("Enter illness:");
                            anotherIllness = Console.ReadLine();
                        }
                        if (anotherIllness != null){
                            previousIllnesses.Add(anotherIllness);
                        }
                    } else if (answerForMoreIllnesses == "no"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong answer! Please try again.");
                    }
                }
                break;
            } else if (answerForIllnesses == "no"){
                break;
            } else {
                Console.WriteLine("You entered wrong answer! Please try again.");
            }
        }
        return previousIllnesses;
    }

    public static List<string> EnterAllergens(){
        List<string> allergens = new List<string>();
        while (true){
            Console.WriteLine("Does patient have any allergens? (enter 'yes' or 'no')");
            var answerForAllergens = Console.ReadLine();
            if (answerForAllergens == "yes"){
                Console.WriteLine("Enter allergen:");
                var allergen = Console.ReadLine();
                while (allergen == ""){
                    Console.WriteLine("Enter allergen:");
                    allergen = Console.ReadLine();
                }
                if (allergen != null){
                    allergens.Add(allergen);
                }
                while (true){
                    Console.WriteLine("Is there more? (enter 'yes' or 'no')");
                    var answerForMoreAllergens = Console.ReadLine();
                    if (answerForMoreAllergens == "yes"){
                        Console.WriteLine("Enter allergen:");
                        var anotherAllergen = Console.ReadLine();
                        while (anotherAllergen == ""){
                            Console.WriteLine("Enter allergen:");
                            anotherAllergen = Console.ReadLine();
                        }
                        if (anotherAllergen != null){
                            allergens.Add(anotherAllergen);
                        }
                    } else if (answerForMoreAllergens == "no"){
                        break;
                    } else {
                        Console.WriteLine("You entered wrong answer! Please try again.");
                    }
                }
                break;
            } else if (answerForAllergens == "no"){
                break;
            } else {
                Console.WriteLine("You entered wrong answer! Please try again.");
            }
        }
        return allergens;
    }

    public void CreateNewAccount(){
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Enter id:");
        var id = Console.ReadLine();
        while (patientsFactory.FindById(Convert.ToInt32(id)) != null){
            Console.WriteLine("There's already a patient with this id. Please enter different one:");
            id = Console.ReadLine();
        }
        Console.WriteLine("Enter name:");
        var name = Console.ReadLine();
        while (name == ""){
            Console.WriteLine("Enter name:");
            name = Console.ReadLine();
        }
        Console.WriteLine("Enter email:");
        var email = Console.ReadLine();
        while (email == ""){
            Console.WriteLine("Enter email:");
            email = Console.ReadLine();
        }
        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();
        while (password == ""){
            Console.WriteLine("Enter password:");
            password = Console.ReadLine();
        }
        Console.WriteLine("Enter height:");
        var height = Console.ReadLine();
        while (height == ""){
            Console.WriteLine("Enter height:");
            height = Console.ReadLine();
        }
        Console.WriteLine("Enter weight:");
        var weight = Console.ReadLine();
        while (weight == ""){
            Console.WriteLine("Enter weight:");
            weight = Console.ReadLine();
        }
        Console.WriteLine("Enter blood type:");
        var bloodType = Console.ReadLine();
        while (bloodType == ""){
            Console.WriteLine("Enter blood type:");
            bloodType = Console.ReadLine();
        }
        List<string> previousIllnesses = EnterIllnesses();
        List<string> allergens = EnterAllergens();
        List<int> recipes = new List<int>();
        if (weight != null && bloodType != null){
            MedicalRecord newMedicalRecord = new MedicalRecord(Convert.ToInt32(height), float.Parse(weight), bloodType, previousIllnesses, allergens);
            if (name != null && email != null && password != null){
            Patient newPatient = new Patient(Convert.ToInt32(id), name, email, password, newMedicalRecord, (Blocked)0, recipes);
            allPatients.Add(newPatient);
        }
        }
        UpdateData();
        Console.WriteLine("You successfully created new patients account!");
    }

    public void ModifyAccount(){
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Enter patient's id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var foundPatient = patientsFactory.FindById(id);
        if (foundPatient == null){
            Console.WriteLine("There's no patient with that id.");
            return;
        }
        while (true){
            Console.WriteLine("1. Name\n2. Email\n3. Password\n4. Finished");
            Console.WriteLine("Enter option:");
            var option = Console.ReadLine();
            if (option == "1"){
                Console.WriteLine("Enter new name:");
                var newName = Console.ReadLine();
                if (foundPatient != null && newName != null){
                    foundPatient.name = newName;
                }
            } else if (option == "2"){
                Console.WriteLine("Enter new email:");
                var newEmail = Console.ReadLine();
                if (foundPatient != null && newEmail != null){
                    foundPatient.email = newEmail;
                }
            } else if (option == "3"){
                Console.WriteLine("Enter new password:");
                var newPassword = Console.ReadLine();
                if (foundPatient != null && newPassword != null){
                    foundPatient.password = newPassword;
                }
            } else if (option == "4"){
                break;
            } else {
                Console.WriteLine("You entered wrong option! Please try again.");
            }
        }
        patientsFactory.UpdateData();
        Console.WriteLine("You successfully modified ", foundPatient.name, "'s account!");
    }

    public void DeleteAccount(){
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Enter patient's id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var foundPatient = patientsFactory.FindById(id);
        if (foundPatient == null){
            Console.WriteLine("You can't delete patient that doesn't exist.");
            return;
        }
        allPatients.Remove(foundPatient);
        patientsFactory.UpdateData();
        Console.WriteLine("You successfully deleted ", foundPatient.name, "'s account!");
    }

    public void ViewAllPatients(){
        Console.WriteLine("Patiens:");
        foreach(Patient patient in allPatients){
            Console.WriteLine(patient);
        }
    }
    
    public void BlockAccount(){
        PatientsFactory patientsFactory = new PatientsFactory();
        Console.WriteLine("Enter patient's id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var foundPatient = patientsFactory.FindById(id);
        if (foundPatient == null){
            Console.WriteLine("You can't block patient that doesn't exist.");
            return;
        }
        foundPatient.blocked = (Blocked)1;
        UpdateData();
        Console.WriteLine("You successfully blocked " + foundPatient.name + "'s account!");
    }

    public void UnblockAccount(){
        Console.WriteLine("Enter patient's id:");
        var id = Convert.ToInt32(Console.ReadLine());
        var foundPatient = FindById(id);
        if (foundPatient == null){
            Console.WriteLine("You can't unblock patient that doesn't exist.");
            return;
        }
        if (foundPatient.blocked != (Blocked)1){
            Console.WriteLine("You can't unblock patient that isn't already blocked.");
            return;
        }
        foundPatient.blocked = (Blocked)0;
        UpdateData();
        Console.WriteLine("You successfully unblocked " + foundPatient.name + "'s account!");
    }

    public void ViewAllBlockedPatients(){
        List<Patient> blockedPatients = new List<Patient>();
        foreach(Patient patient in allPatients){
            if (patient.blocked == (Blocked)1){
                blockedPatients.Add(patient);
            }
        }
        if (blockedPatients.Count() == 0){
            Console.WriteLine("There's none blocked patients!");
        } else {
            Console.WriteLine("Blocked patients:");
            foreach(Patient patient in blockedPatients){
                Console.WriteLine(patient);
            }
        }
    }

    public void ViewAllPatientsWithReferral(){
        List<Patient> patientsWithReferral = new List<Patient>();
        foreach(Patient patient in allPatients){
            if(patient.referralsId != 0){
                patientsWithReferral.Add(patient);
            }
        }
        if (patientsWithReferral.Count() == 0){
            Console.WriteLine("There's none patients with referrals!");
        } else {
            Console.WriteLine("Patients with referral:");
            foreach(Patient patient in patientsWithReferral){
                Console.WriteLine(patient);
            }
        }
    }

}