namespace Roomed.Common.Attribues
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SanitizeAttribute : Attribute
    {
    }
}
