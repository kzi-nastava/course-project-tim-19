using Newtonsoft.Json;
public class DeletionRequestsFactory{
    public static List<DeletionRequest> allDeletionRequests { get; set; } = null!;

    public DeletionRequestsFactory(){
        var deletionRequests = JsonConvert.DeserializeObject<List<DeletionRequest>>(File.ReadAllText("Data/deletionRequests.json"));
        if (deletionRequests != null){
            allDeletionRequests = deletionRequests;
        }
    }

    public List<DeletionRequest> GetAllDeletionRequests(){
        return allDeletionRequests;
    }

    public void UpdateData(){
        var convertedRequests = JsonConvert.SerializeObject(allDeletionRequests, Formatting.Indented);
        File.WriteAllText("Data/deletionRequests.json", convertedRequests);
    }

    public DeletionRequest FindById(int id){
        foreach (DeletionRequest request in allDeletionRequests){
            if (request.appointmentsId == id){
                return request;
            }
        }
        return null;
    }

    public static void ViewAll(){
        foreach(DeletionRequest request in allDeletionRequests){
            Console.WriteLine(request);
        }
    }

    public void ManageDeletionRequests() {
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory();
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        DeletionRequestsFactory deletionRequestsFactory = new DeletionRequestsFactory();
        if (allDeletionRequests.Count() == 0){
            Console.WriteLine("There's none deletion requests!");
            return;
        } else {
            Console.WriteLine("Deletion requests:");
            ViewAll();
            Console.WriteLine("Enter id of request you want to solve: ");
            var id = Console.ReadLine();
            DeletionRequest foundRequest = deletionRequestsFactory.FindById(Convert.ToInt32(id));
            if (foundRequest == null){
                Console.WriteLine("There's no request with that id.");
                return;
            }
            while (true){
                Console.WriteLine("1. Approve request\n2. Decline request");
                Console.WriteLine("Enter the option: ");
                var choice = Console.ReadLine();
                if (choice == "1"){
                    doctorAppointmentsFactory.Delete(Convert.ToInt32(id));
                    allDeletionRequests.Remove(foundRequest);
                    deletionRequestsFactory.UpdateData();
                    Console.WriteLine("You successfully approved request!");
                    break;
                } else if (choice == "2"){
                    allDeletionRequests.Remove(foundRequest);
                    deletionRequestsFactory.UpdateData();
                    Console.WriteLine("You successfully declined request!");
                    break;
                } else {
                    Console.WriteLine("You entered wrong option! Please try again.");
                }
            }
        }
    }

}