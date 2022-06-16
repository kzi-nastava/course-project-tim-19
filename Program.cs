class Program{

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

                DoctorsFactory doctorsFactory = new DoctorsFactory();
                SecretariesFactory secretariesFactory = new SecretariesFactory();

                foreach (var doctor in doctorsFactory.GetAllDoctors()){
                    if(doctor.email == email && doctor.password == password){
                        Doctor.doctorMenu(doctor);
                    }
                }
                foreach (var secretary in secretariesFactory.GetAllSecretaries()){
                    if(secretary.email == email && secretary.password == password){
                        Secretary.Menu(secretary);
                    }
                }
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
