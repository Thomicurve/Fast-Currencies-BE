﻿namespace fast_currencies_be;

public class SubscriptionDto
{   
    public int Id  { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal MaxRequestsPerMonth { get; set; }

}
