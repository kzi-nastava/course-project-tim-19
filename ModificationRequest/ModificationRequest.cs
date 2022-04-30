using Newtonsoft.Json;
public class ModificationRequest{

    public int appointmentsId { get; set; }
    public int doctorsId { get; set; }
    public DateTime dateTime { get; set; }
    public Emergency emergency { get; set; }

    public ModificationRequest(int id, int doctorsId, DateTime dateTime, Emergency emergency){
        this.appointmentsId = id;
        this.doctorsId = doctorsId;
        this.dateTime = dateTime;
        this.emergency = emergency;
    }

    public override string ToString()
    {
        return "ModificationRequest [appointmentsId: " + appointmentsId + "]";
    }
    
    public static ModificationRequest FindById(int id, List<ModificationRequest> allRequests){
        foreach (ModificationRequest request in allRequests){
            if (request.appointmentsId == id){
                return request;
            }
        }
        return null;
    }

    public static void UpdateData(List<ModificationRequest> allRequests){
        var convertedRequests = JsonConvert.SerializeObject(allRequests, Formatting.Indented);
        File.WriteAllText("Data/modificationRequests.json", convertedRequests);
    }

    public static void ManageRequest(List<ModificationRequest> allRequests){
        DoctorAppointmentsFactory doctorAppointmentsFactory = new DoctorAppointmentsFactory("Data/doctorAppointments.json");
        DoctorsFactory doctorsFactory = new DoctorsFactory("Data/doctors.json");
        if (allRequests.Count() == 0){
            Console.WriteLine("There's none modification requests!");
            return;
        } else {
            Console.WriteLine("Modification requests:");
            foreach(ModificationRequest request in allRequests){
                Console.WriteLine(request);
            }
            Console.WriteLine("Enter id of request you want to solve: ");
            var id = Console.ReadLine();
            ModificationRequest foundRequest = FindById(Convert.ToInt32(id), allRequests);
            if (foundRequest == null){
                Console.WriteLine("There's no request with that id.");
                return;
            }
            while (true){
                Console.WriteLine("1. Approve request\n2. Decline request");
                Console.WriteLine("Enter the option: ");
                var choice = Console.ReadLine();
                if (choice == "1"){
                    DoctorAppointment.Update(foundRequest, doctorAppointmentsFactory.allDoctorAppointments, doctorsFactory.allDoctors);
                    allRequests.Remove(foundRequest);
                    UpdateData(allRequests);
                    Console.WriteLine("You successfully approved request!");
                    break;
                } else if (choice == "2"){
                    allRequests.Remove(foundRequest);
                    UpdateData(allRequests);
                    Console.WriteLine("You successfully declined request!");
                    break;
                } else {
                    Console.WriteLine("You entered wrong option! Please try again.");
                }
            }
        }
    }

}
