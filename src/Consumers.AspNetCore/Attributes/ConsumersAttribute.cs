namespace Consumers.AspNetCore.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ConsumersAttribute : Attribute
{
    public String[] Consumers { get; }

    public ConsumersAttribute(params String[] consumers)
    {
        Consumers = consumers;
    }
}