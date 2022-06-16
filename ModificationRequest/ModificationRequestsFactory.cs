using Newtonsoft.Json;
public class ModificationRequestsFactory{
    public static List<ModificationRequest> allModificationRequests { get; set; } = null!;

    public ModificationRequestsFactory(){
        var modificationRequests = JsonConvert.DeserializeObject<List<ModificationRequest>>(File.ReadAllText("Data/modificationRequests.json"));
        if (modificationRequests != null){
            allModificationRequests = modificationRequests;
        }
    }

    public List<ModificationRequest> GetAllModificationRequests(){
        return allModificationRequests;
    }

    public static ModificationRequest FindById(int id){
        foreach (ModificationRequest request in allModificationRequests){
            if (request.appointmentsId == id){
                return request;
            }
        }
        return null;
    }

    public static void UpdateData(){
        var convertedRequests = JsonConvert.SerializeObject(allModificationRequests, Formatting.Indented);
        File.WriteAllText("Data/modificationRequests.json", convertedRequests);
    }

    public static void ViewAll(){
        foreach(ModificationRequest request in allModificationRequests){
                Console.WriteLine(request);
        }
    }

    public static void ApproveModificationRequest(ModificationRequest foundRequest){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        doctorAppointmentsFactory.Update(foundRequest);
        allModificationRequests.Remove(foundRequest);
        UpdateData();
        Console.WriteLine("You successfully approved request!");
    }

    public static void DeclineModificationRequest(ModificationRequest foundRequest){
        allModificationRequests.Remove(foundRequest);
        UpdateData();
        Console.WriteLine("You successfully declined request!");
    }

    public void ManageRequest(){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        if (allModificationRequests.Count() == 0){
            Console.WriteLine("There's none modification requests!");
            return;
        } else {
            Console.WriteLine("Modification requests:");
            ViewAll();
            Console.WriteLine("Enter id of request you want to solve: ");
            var id = Console.ReadLine();
            ModificationRequest foundRequest = FindById(Convert.ToInt32(id));
            if (foundRequest == null){
                Console.WriteLine("There's no request with that id.");
                return;
            }
            while (true){
                Console.WriteLine("1. Approve request\n2. Decline request");
                Console.WriteLine("Enter the option: ");
                var choice = Console.ReadLine();
                if (choice == "1"){
                    ApproveModificationRequest(foundRequest);
                    break;
                } else if (choice == "2"){
                    DeclineModificationRequest(foundRequest);
                    break;
                } else {
                    Console.WriteLine("You entered wrong option! Please try again.");
                }
            }
        }
    }

}