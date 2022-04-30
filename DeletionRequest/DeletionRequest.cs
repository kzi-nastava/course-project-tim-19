using Newtonsoft.Json;
public class DeletionRequest{

    public int appointmentsId;

    public DeletionRequest(int id){
        this.appointmentsId = id;
    }

    public override string ToString()
    {
        return "DeletionRequest [appointmentsId: " + appointmentsId + "]";
    }
    
    public static DeletionRequest FindDeletionRequestById(int id, List<DeletionRequest> allRequests){
        foreach (DeletionRequest request in allRequests){
            if (request.appointmentsId == id){
                return request;
            }
        }
        return null;
    }

    public static void UpdateDeletionRequestsData(List<DeletionRequest> allRequests){
        var convertedRequests = JsonConvert.SerializeObject(allRequests, Formatting.Indented);
        File.WriteAllText("Data/deletionRequests.json", convertedRequests);
    }

    public static void ManageDeletionRequests(List<DeletionRequest> allRequests){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        DoctorsFactory doctorsFactory = new DoctorsFactory("Data/doctors.json");
        if (allRequests.Count() == 0){
            Console.WriteLine("There's none deletion requests!");
            return;
        } else {
            Console.WriteLine("Deletion requests:");
            foreach(DeletionRequest request in allRequests){
                Console.WriteLine(request);
            }
            Console.WriteLine("Enter id of request you want to solve: ");
            var id = Console.ReadLine();
            DeletionRequest foundRequest = FindDeletionRequestById(Convert.ToInt32(id), allRequests);
            if (foundRequest == null){
                Console.WriteLine("There's no request with that id.");
                return;
            }
            while (true){
                Console.WriteLine("1. Approve request\n2. Decline request");
                Console.WriteLine("Enter the option: ");
                var choice = Console.ReadLine();
                if (choice == "1"){
                    DoctorAppointment.Delete(Convert.ToInt32(id), doctorAppointmentsFactory.allDoctorAppointments, doctorsFactory.allDoctors);
                    allRequests.Remove(foundRequest);
                    UpdateDeletionRequestsData(allRequests);
                    Console.WriteLine("You successfully approved request!");
                    break;
                } else if (choice == "2"){
                    allRequests.Remove(foundRequest);
                    UpdateDeletionRequestsData(allRequests);
                    Console.WriteLine("You successfully declined request!");
                    break;
                } else {
                    Console.WriteLine("You entered wrong option! Please try again.");
                }
            }
        }
    }
}
