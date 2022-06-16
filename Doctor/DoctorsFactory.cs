using Newtonsoft.Json;
public class DoctorsFactory{
    public static List<Doctor> allDoctors { get; set; } = null!;

    public DoctorsFactory(){
        var doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText("Data/doctors.json"));
        if (doctors != null){
            allDoctors = doctors;
        }
    }

    public List<Doctor> GetAllDoctors(){
        return allDoctors;
    }

    public void UpdateData(){
        var convertedDoctors = JsonConvert.SerializeObject(allDoctors, Formatting.Indented);
        File.WriteAllText("Data/doctors.json", convertedDoctors);
    }

    public Doctor FindById(int id){
        foreach (Doctor doctor in allDoctors){
            if (doctor.id == id){
                return doctor;
            }
        }
        return null;
    }

    public Doctor FindByField(Field field){
        foreach (Doctor doctor in allDoctors){
            if (doctor.field == field){
                return doctor;
            }
        }
        return null;
    }

}
