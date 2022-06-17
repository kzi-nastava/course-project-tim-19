using Newtonsoft.Json;
public class MedicinesFactory{
    public static List<Medicine> allMedicines { get; set; } = null!;

    public MedicinesFactory(string data){
        var medicines = JsonConvert.DeserializeObject<List<Medicine>>(File.ReadAllText(data));
        if (medicines != null){
            allMedicines = medicines;
        }
    }

    public void UpdateData(string data){
        var convertedMedicines = JsonConvert.SerializeObject(allMedicines, Formatting.Indented);
        File.WriteAllText(data, convertedMedicines);
    }

    public static void DeleteMedicineRequest(Medicine medicine){
        MedicinesFactory medicinesFactory=new MedicinesFactory("Data/medicineRequests.json");
        
        medicinesFactory.GetAllMedicines().Remove(medicine);
        
    }

    public List<Medicine> GetAllMedicines(){
        return allMedicines;
    }

    public static void VerifyMedicine(Medicine medicineToVerify){
                
        Console.WriteLine("Enter option:\n1. Accept\n2. Reject\n3. Exit");
        int entry= Convert.ToInt32(Console.ReadLine());
        
        if (entry==1){
            
            MedicinesFactory medicinesFactory = new MedicinesFactory("Data/medicines.json");
            medicinesFactory.GetAllMedicines().Add(medicineToVerify);
            medicinesFactory.UpdateData("Data/medicines.json");
            Console.WriteLine("Medicine accepted successfully.");

            MedicinesFactory.DeleteMedicineRequest(medicineToVerify);

        }else if(entry ==2){
            Console.WriteLine("Add comment: ");
            var comment=Console.ReadLine();
            RejectedMedicinesFactory rejectedMedicinesFactory = new RejectedMedicinesFactory();
            RejectedMedicine rejectedMedicine=new RejectedMedicine(medicineToVerify,comment);
            rejectedMedicinesFactory.GetAllRejectedMedicines().Add(rejectedMedicine);
            rejectedMedicinesFactory.UpdateData();

            MedicinesFactory medicineRequestsFactory = new MedicinesFactory("Data/medicineRequests.json");
            
            MedicinesFactory.DeleteMedicineRequest(medicineToVerify);
            
        }else{
            Console.WriteLine("Wrong character entered.");
        }
        
    }

}