using Newtonsoft.Json;
public class PatientsFactory{
    public List<Patient> allPatients { get; set; } = null!;

    public PatientsFactory(string path){
        var patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(path));
        if (patients != null){
            allPatients = patients;
        }
    }

    public static void UpdatePatients(List<Patient> allPatients){
        var convertedPatients = JsonConvert.SerializeObject(allPatients, Formatting.Indented);
        File.WriteAllText("Data/patients.json", convertedPatients);
    }
}