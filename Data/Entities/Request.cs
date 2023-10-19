namespace fast_currencies_be;

public class Request : EntityBase
{
    public int CurrentRequests { get; set; } = 0;
    public DateTime? FirstRequestFromMonthDate { get; set; }
    public int UserId { get; set; }
}
