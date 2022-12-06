// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;

// Copied from https://github.com/dotnet/efcore/blob/main/src/Shared/Check.cs
namespace Movies.Core.Utils
{
    public static class Check
    {
        public static T NotNull<T>([DisallowNull, NotNull] T value, [NotNull] string parameterName)
        {
            if (value == null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T NotNull<T>(
            [NotNull] T value,
            [NotNull] string parameterName,
            [NotNull] string propertyName)
        {
            if (value == null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));

                throw new ArgumentException(CoreStrings.ArgumentPropertyNull(propertyName, parameterName));
            }

            return value;
        }

        public static string? NotEmpty(string value, [NotNull] string parameterName)
        {
            Exception? e = null;
            if (value is null)
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(CoreStrings.ArgumentIsEmpty(parameterName));
            }

            if (e != null)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw e;
            }

            return value;
        }

        public static string? NotEmpty(
            string value,
            [NotNull] string parameterName,
            [NotNull] string propertyName)
        {
            Exception? e = null;
            if (value is null)
            {
                e = new ArgumentException(CoreStrings.ArgumentPropertyNull(propertyName, parameterName));
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException(CoreStrings.ArgumentPropertyEmpty(propertyName, parameterName));
            }

            if (e != null)
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));

                throw e;
            }
            return value;
        }

        public static int GreaterThanZero(int value, [NotNull] string parameterName)
        {
            if (value <= 0)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentOutOfRangeException(CoreStrings.ArgumentIsLowerOrEqualZero(parameterName));
            }

            return value;
        }

        public static IList<T> HasNoNulls<T>(IList<T> value, [NotNull] string parameterName)
        {
            NotNull(value, parameterName);
            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }
    }
}