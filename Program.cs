class Program{
    public static void findDoctor(string email,string password){
    
        DoctorsFactory doctorsFactory = new DoctorsFactory();
        foreach (var doctor in doctorsFactory.GetAllDoctors()){
                        if(doctor.email == email && doctor.password == password){
                            Doctor.doctorMenu(doctor);
                        }
                    }
    }
    public static void findSecretary(string email,string password){
    
        SecretariesFactory secretariesFactory = new SecretariesFactory();
        foreach (var secretary in secretariesFactory.GetAllSecretaries()){
                        if(secretary.email == email && secretary.password == password){
                            Secretary.Menu(secretary);
                        }
                    }
    }
    public static void login(){
        while(true){
            Console.WriteLine("LOGIN");
            Console.WriteLine("Do you want to log in?");
            Console.WriteLine("Enter 'yes' or 'no':");
            var answer = Console.ReadLine();
            if (answer == "yes"){
                Console.WriteLine("Email: ");
                var email = Console.ReadLine();
                Console.WriteLine("Password: ");
                var password = Console.ReadLine();

                if(email != null && password != null){
                    email = Convert.ToString(email);
                    password = Convert.ToString(password);
                }

                findDoctor(email,password);
                findSecretary(email,password);
                
            } else if (answer == "no"){
                break;
            } else {
                Console.WriteLine("You entered wrond answer! Please try again.");
            }
        }
    }

    static void Main (string[] args){
        login();
    }

}
