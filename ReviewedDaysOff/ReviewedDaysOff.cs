public class ReviewedDaysOff{

    public int requestsId { get; set;}
    public bool approved { get; set; }
    public string explanation { get; set; }

    public ReviewedDaysOff(int requestsId, bool approved){
        this.requestsId = requestsId;
        this.approved = approved;
        this.explanation = "";
    }

    public ReviewedDaysOff(int requestsId, bool approved, string explanation){
        this.requestsId = requestsId;
        this.approved = approved;
        this.explanation = explanation;
    }

}