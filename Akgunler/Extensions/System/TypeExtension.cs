using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Extensions.System
{
    public static class TypeExtension
    {
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
