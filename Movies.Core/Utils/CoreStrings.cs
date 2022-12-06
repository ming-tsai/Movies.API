using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

// copied from https://github.com/dotnet/efcore/blob/main/src/EFCore/Properties/CoreStrings.resx
namespace Movies.Core.Utils
{
    internal static class CoreStrings
    {
        /// <summary>
        /// The property '{property}' of the argument '{argument}' cannot be null.
        /// </summary>
        public static string ArgumentPropertyNull([AllowNull] string property, [AllowNull] string argument)
        {
            return string.Format(CultureInfo.CurrentCulture, "The property '{0}' of the argument '{1}' cannot be null.", property, argument);
        }

        /// <summary>
        /// The property '{property}' of the argument '{argument}' cannot be null.
        /// </summary>
        public static string ArgumentPropertyEmpty([AllowNull] string property, [AllowNull] string argument)
        {
            return string.Format(CultureInfo.CurrentCulture, "The property '{0}' of the argument '{1}' cannot be empty.", property, argument);
        }

        /// <summary>
        /// The string argument '{argumentName}' cannot be empty.
        /// </summary>
        public static string ArgumentIsEmpty([AllowNull] string argumentName)
        {
            return string.Format(CultureInfo.CurrentCulture, "The string argument '{0}' cannot be empty.", argumentName);
        }

        /// <summary>
        /// The number argument '{0}' should be greater than zero.
        /// </summary>
        public static string ArgumentIsLowerOrEqualZero([AllowNull] string argumentName)
        {
            return string.Format(CultureInfo.CurrentCulture, "The number argument '{0}' should be greater than zero.", argumentName);
        }
    }
}
