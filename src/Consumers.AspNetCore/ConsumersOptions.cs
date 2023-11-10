namespace Consumers.AspNetCore;

public class ConsumersOptions
{
    public String HeaderName { get; set; } = "X-Consumer";

    public List<String> Consumers { get; set; } = new();
}