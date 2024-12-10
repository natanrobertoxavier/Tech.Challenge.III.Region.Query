namespace Region.Query.Communication.Response;

public class ResponseRegionDDDJson(
    int dDD, 
    string region)
{
    public int DDD { get; set; } = dDD;
    public string Region { get; set; } = region;
}
