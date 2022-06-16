using Newtonsoft.Json;
public class DoctorsFactory{
    public List<Doctor> allDoctors { get; set; } = null!;

    public DoctorsFactory(string path){
        var doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(path));
        if (doctors != null){
            allDoctors = doctors;
        }
    }

    public static void UpdateDoctors(List<Doctor> allDoctors){
        var convertedDoctors = JsonConvert.SerializeObject(allDoctors, Formatting.Indented);
        File.WriteAllText("Data/doctors.json", convertedDoctors);
    }

}
