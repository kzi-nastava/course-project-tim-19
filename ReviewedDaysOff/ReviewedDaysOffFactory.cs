using Newtonsoft.Json;
public class ReviewedDaysOffFactory{
    public static List<ReviewedDaysOff> allReviewedDaysOff { get; set; } = null!;

    public ReviewedDaysOffFactory(){
        var reviewedDaysOff = JsonConvert.DeserializeObject<List<ReviewedDaysOff>>(File.ReadAllText("Data/reviewedDaysOff.json"));
        if (reviewedDaysOff != null){
            allReviewedDaysOff = reviewedDaysOff;
        }
    }

    public static void UpdateData(){
        var convertedReviewedDaysOff = JsonConvert.SerializeObject(allReviewedDaysOff, Formatting.Indented);
        File.WriteAllText("Data/reviewedDaysOff.json", convertedReviewedDaysOff);
    }

    public List<ReviewedDaysOff> GetAllRevieweDaysOff(){
        return allReviewedDaysOff;
    }

    public void ReviewDayOffRequests(){
        DayOffRequestsFactory daysOffRequestsFactory = new DayOffRequestsFactory();
        Console.WriteLine("All requests: ");
        daysOffRequestsFactory.ViewAll();
        Console.WriteLine("Enter id of request you want to review.");
        var idOfRequest = Console.ReadLine();
        DayOffRequest foundRequest = DayOffRequestsFactory.FindById(Convert.ToInt32(idOfRequest));
        Console.WriteLine("Do you want to:\n1. approve\n2. decline");
        Console.WriteLine("Enter the option: ");
        var optionForApproving = Console.ReadLine();
        if (optionForApproving == "1"){
            ReviewedDaysOff reviewedDaysOff = new ReviewedDaysOff(Convert.ToInt32(idOfRequest), true);
            ReviewedDaysOffFactory.allReviewedDaysOff.Add(reviewedDaysOff);
            ReviewedDaysOffFactory.UpdateData();
        } else if (optionForApproving == "2"){
            Console.WriteLine("Please enter the explanation for declining the request.");
            var explanationForDeclining = Console.ReadLine();
            if (explanationForDeclining != null){
                ReviewedDaysOff reviewedDaysOff = new ReviewedDaysOff(Convert.ToInt32(idOfRequest), false, explanationForDeclining);
                ReviewedDaysOffFactory.allReviewedDaysOff.Add(reviewedDaysOff);
                ReviewedDaysOffFactory.UpdateData();
            }
        } else {
            Console.WriteLine("You entered wrong option! Please try again.");
        }
    }

}