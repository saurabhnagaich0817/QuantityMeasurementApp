using System;  // Add this line at top
using ModelLayer.Enums;

namespace ModelLayer.Interfaces
{
    public interface IMeasurable<TUnit>: IUnitConverter<TUnit> where TUnit: struct, Enum
    {
        double GetConversionFactor(TUnit unit);
    }
}
