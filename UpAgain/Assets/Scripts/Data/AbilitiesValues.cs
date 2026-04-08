using System.Collections.Generic;

public enum AbilityType
{
    TurboSpeed,
    Shield,
    Freeze
}

public class AbilitiesValues
{
    public static Dictionary<AbilityType, int> values = new Dictionary<AbilityType, int>()
    {
        { AbilityType.TurboSpeed, 0 },
        { AbilityType.Shield, 0 },
        { AbilityType.Freeze, 0 }
    };
}
