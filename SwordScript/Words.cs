namespace SwordScript;

public static class Words
{
    public static readonly string[] ALL_RESERVED_WORDS = new[]
    {
        BOOLEAN_TRUE,
        BOOLEAN_FALSE,
        NULL,
        NOT,
        AND,
        OR
    };
    
    public const string BOOLEAN_TRUE = "true";
    public const string BOOLEAN_FALSE = "false";
    public const string NULL = "null";
    public const string NOT = "not";
    public const string AND = "and";
    public const string OR = "or";
}