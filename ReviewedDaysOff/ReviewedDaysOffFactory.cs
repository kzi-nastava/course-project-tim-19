using Newtonsoft.Json;
public class ReviewedDaysOffFactory{
    public static List<ReviewedDaysOff> allReviewedDaysOff { get; set; } = null!;

    public ReviewedDaysOffFactory(string path){
        var reviewedDaysOff = JsonConvert.DeserializeObject<List<ReviewedDaysOff>>(File.ReadAllText(path));
        if (reviewedDaysOff != null){
            allReviewedDaysOff = reviewedDaysOff;
        }
    }

    public static void UpdateReviewedDayOff(List<ReviewedDaysOff> allReviewedDaysOff){
        var convertedReviewedDaysOff = JsonConvert.SerializeObject(allReviewedDaysOff, Formatting.Indented);
        File.WriteAllText("Data/reviewedDaysOff.json", convertedReviewedDaysOff);
    }

    public static void ReviewDayOffRequests(){
                Console.WriteLine("All requests: ");
                DayOffRequestsFactory.ViewAll();
                Console.WriteLine("Enter id of request you want to review.");
                var idOfRequest = Console.ReadLine();
                DayOffRequest foundRequest = DayOffRequest.FindById(Convert.ToInt32(idOfRequest), DayOffRequestsFactory.allDayOffRequests);
                Console.WriteLine("Do you want to:\n1. approve\n2. decline");
                Console.WriteLine("Enter the option: ");
                var optionForApproving = Console.ReadLine();
                if (optionForApproving == "1"){
                    ReviewedDaysOff reviewedDaysOff = new ReviewedDaysOff(Convert.ToInt32(idOfRequest), true);
                    ReviewedDaysOffFactory.allReviewedDaysOff.Add(reviewedDaysOff);
                    ReviewedDaysOffFactory.UpdateReviewedDayOff(ReviewedDaysOffFactory.allReviewedDaysOff);
                } else if (optionForApproving == "2"){
                    Console.WriteLine("Please enter the explanation for declining the request.");
                    var explanationForDeclining = Console.ReadLine();
                    if (explanationForDeclining != null){
                        ReviewedDaysOff reviewedDaysOff = new ReviewedDaysOff(Convert.ToInt32(idOfRequest), false, explanationForDeclining);
                        ReviewedDaysOffFactory.allReviewedDaysOff.Add(reviewedDaysOff);
                        ReviewedDaysOffFactory.UpdateReviewedDayOff(ReviewedDaysOffFactory.allReviewedDaysOff);                    
                    }
                } else {
                    Console.WriteLine("You entered wrong option! Please try again.");
                }
    }

}