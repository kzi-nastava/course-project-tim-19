using Newtonsoft.Json;
public class DayOffRequestsFactory{
    public static List<DayOffRequest> allDayOffRequests { get; set; } = null!;

    public DayOffRequestsFactory(){
        var dayOffRequests = JsonConvert.DeserializeObject<List<DayOffRequest>>(File.ReadAllText("Data/dayOffRequests.json"));
        if (dayOffRequests != null){
            allDayOffRequests = dayOffRequests;
        }
    }

    public void UpdateData(){
        var convertedDayOffRequests = JsonConvert.SerializeObject(allDayOffRequests, Formatting.Indented);
        File.WriteAllText("Data/dayOffRequests.json", convertedDayOffRequests);
    }

    public List<DayOffRequest> GetAllDaysOffRequests(){
        return allDayOffRequests;
    }

    public void ViewAll(){
        foreach (DayOffRequest request in allDayOffRequests){
            Console.WriteLine(request);
        }
    }

    public static int FindNextId(){
        List<int> allIds = new List<int>();
        foreach (DayOffRequest request in allDayOffRequests){
            allIds.Add(request.id);
        }
        for (int i = 0; i < 100; ++i){
            if (!allIds.Contains(i)){
                return i;
            }
        }
        return 0;
    }

    public static DayOffRequest FindById(int id){
        foreach (DayOffRequest request in allDayOffRequests){
            if (request.id == id){
                return request;
            }
        }
        return null;
    }

}