using Newtonsoft.Json;
public class DoctorsFactory{
    public List<Doctor> allDoctors { get; set; } = null!;

    public DoctorsFactory(string path){
        var doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(path));
        if (doctors != null){
            allDoctors = doctors;
        }
    }
}
