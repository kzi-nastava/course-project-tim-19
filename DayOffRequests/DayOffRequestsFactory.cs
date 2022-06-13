using Newtonsoft.Json;
public class DayOffRequestsFactory{
    public List<DayOffRequest> allDayOffRequests { get; set; } = null!;

    public DayOffRequestsFactory(string path){
        var dayOffRequests = JsonConvert.DeserializeObject<List<DayOffRequest>>(File.ReadAllText(path));
        if (dayOffRequests != null){
            allDayOffRequests = dayOffRequests;
        }
    }

    public static void UpdateDayOffRequests(List<DayOffRequest> allDayOffRequests){
        var convertedDayOffRequests = JsonConvert.SerializeObject(allDayOffRequests, Formatting.Indented);
        File.WriteAllText("Data/dayOffRequests.json", convertedDayOffRequests);
    }
}