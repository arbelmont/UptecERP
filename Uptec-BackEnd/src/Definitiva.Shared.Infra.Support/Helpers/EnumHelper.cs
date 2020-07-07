using System;

namespace Definitiva.Shared.Infra.Support.Helpers
{
    public static class EnumHelper
    {
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {

            Enum.TryParse(value, true, out TEnum resultInputType);

            // TODO (By marcone): Verificar como alimentar notification  
            // if (!Enum.TryParse(value, true, out TEnum resultInputType))
            // ...

            return resultInputType;
        }
    }
}
