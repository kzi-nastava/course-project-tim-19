public class DayOffRequest
{
    public int id { get; set; }
    public int doctorId { get; set; }
    public string date { get; set; }
    public int duration { get; set; }
    public string state { get; set; }
    public string cause { get; set; }

    public DayOffRequest(int id, int doctorId, string date, int duration,string state,string cause){
        this.id = id;
        this.doctorId = doctorId;
        this.date = date;
        this.duration = duration;
        this.state = state;
        this.cause = cause;
    }

    public override string ToString()
    {
        return "DayOffRequest [id:" + id + "doctor id:" + doctorId+", date: "+date+", duration: "+duration+", state: "+state+", cause: "+cause +"]";
        
    }

    public static int FindNextIdForRequest(List<DayOffRequest> allRequests){
        List<int> allIds = new List<int>();
        foreach (DayOffRequest request in allRequests){
            allIds.Add(request.id);
        }
        for (int i = 0; i < 100; ++i){
            if (!allIds.Contains(i)){
                return i;
            }
        }
        return 0;
    }

        public static DayOffRequest FindById(int id, List<DayOffRequest> allDayOffRequests){
        foreach (DayOffRequest request in allDayOffRequests){
            if (request.id == id){
                return request;
            }
        }
        return null;
    }

}