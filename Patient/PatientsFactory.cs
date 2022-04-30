using Newtonsoft.Json;
public class PatientsFactory{
    public List<Patient> allPatients { get; set; } = null!;

    public PatientsFactory(string path){
        var patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(path));
        if (patients != null){
            allPatients = patients;
        }
    }
}