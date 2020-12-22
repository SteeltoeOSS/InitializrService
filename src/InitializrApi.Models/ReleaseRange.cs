// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.InitializrApi.Models.Utilities;
using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Steeltoe.InitializrApi.Models
{
    /// <summary>
    /// A model of a release range.
    /// </summary>
    [JsonConverter(typeof(ReleaseRangeJsonConverter))]
    public sealed class ReleaseRange
    {
        /* ----------------------------------------------------------------- *
         * fields                                                            *
         * ----------------------------------------------------------------- */

        private readonly ReleaseVersion _start;

        private readonly bool _startInclusive;

        private readonly ReleaseVersion _stop;

        private readonly bool _stopInclusive;

        /* ----------------------------------------------------------------- *
         * constructors                                                      *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseRange"/> class.
        /// </summary>
        public ReleaseRange()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseRange"/> class.
        /// </summary>
        /// <param name="range">Release version range expression.</param>
        /// <exception cref="ArgumentException">If the range expression or a release version is invalid.</exception>
        public ReleaseRange(string range)
        {
            if (string.IsNullOrEmpty(range))
            {
                return;
            }

            var versions = range.Split(',').Select(v => v.Trim()).ToArray();
            switch (versions.Length)
            {
                case 1:
                    _start = new ReleaseVersion(versions[0].Trim());
                    _startInclusive = true;
                    break;
                case 2:
                    switch (versions[0][0])
                    {
                        case '[':
                            _startInclusive = true;
                            break;
                        case '(':
                            _startInclusive = false;
                            break;
                        default:
                            throw new ArgumentException(
                                $"Release range start version must begin with '[' or '(': '{versions[0]}'");
                    }

                    _start = new ReleaseVersion(versions[0].Substring(1));
                    switch (versions[1][versions[1].Length - 1])
                    {
                        case ']':
                            _stopInclusive = true;
                            break;
                        case ')':
                            _stopInclusive = false;
                            break;
                        default:
                            throw new ArgumentException(
                                $"Release range stop version must end with ']' or ')': '{versions[1]}'");
                    }

                    _stop = new ReleaseVersion(versions[1].Substring(0, versions[1].Length - 1));
                    break;
                default:
                    throw new ArgumentException($"Release range cannot contain more than 2 versions: '{range}'");
            }
        }

        /* ----------------------------------------------------------------- *
         * methods                                                           *
         * ----------------------------------------------------------------- */

        /// <summary>
        /// Tests if a version falls within range.
        /// </summary>
        /// <param name="version">Version to be tested.</param>
        /// <returns>Whether version fall within range.</returns>
        public bool Accepts(string version)
        {
            var releaseVersion = new ReleaseVersion(version);

            if (_start is null)
            {
                return true;
            }

            if (releaseVersion < _start)
            {
                return false;
            }

            if (releaseVersion == _start && !_startInclusive)
            {
                return false;
            }

            if (_stop is null)
            {
                return true;
            }

            if (releaseVersion > _stop)
            {
                return false;
            }

            if (releaseVersion == _stop && !_stopInclusive)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var buf = new StringBuilder();
            if (_stop is null)
            {
                buf.Append(_start);
            }
            else
            {
                buf.Append(_startInclusive ? '[' : '(')
                    .Append(_start);
                buf.Append(',')
                    .Append(_stop)
                    .Append(_stopInclusive ? ']' : ')');
            }

            return buf.ToString();
        }

        /// <summary>
        /// Returns a human-readable representation.
        /// </summary>
        /// <returns>A pretty string.</returns>
        public string ToPrettyString()
        {
            var buf = new StringBuilder();
            if (!(_start is null))
            {
                buf.Append(">")
                    .Append(_startInclusive ? "=" : string.Empty)
                    .Append(_start);
                if (!(_stop is null))
                {
                    buf.Append(" and <")
                        .Append(_stopInclusive ? "=" : string.Empty)
                        .Append(_stop);
                }
            }

            return buf.ToString();
        }

        /// <summary>
        /// Similar to <see cref="Version"/> but with added support for prefixes, e.g. netcoreapp2.1.
        /// </summary>
        public class ReleaseVersion
        {
            private readonly string _representation;

            // ReSharper disable once NotAccessedField.Local
            private readonly string _prefix;

            private readonly Version _version;

            /// <summary>
            /// Initializes a new instance of the <see cref="ReleaseVersion"/> class.
            /// </summary>
            public ReleaseVersion(string version)
            {
                if (string.IsNullOrEmpty(version))
                {
                    throw new ArgumentException("Release version cannot be empty or null");
                }

                try
                {
                    if (char.IsLetter(version[0]))
                    {
                        int prefixEnd = version.IndexOfAny("0123456789".ToCharArray());
                        if (prefixEnd < 0)
                        {
                            throw new ArgumentException($"Release range must contain 1 or 2 versions: '{version}'");
                        }

                        _prefix = version.Substring(0, prefixEnd);
                        _version = new Version(version.Substring(prefixEnd));
                    }
                    else
                    {
                        _prefix = string.Empty;
                        _version = new Version(version);
                    }
                }
                catch (FormatException)
                {
                    throw new ArgumentException($"Version not in correct format: '{version}'");
                }

                _representation = version;
            }

            /// <summary>
            /// Returns <see cref="CompareTo"/> &lt; 0.
            /// </summary>
            /// <param name="a">Left operand.</param>
            /// <param name="b">Right operand.</param>
            /// <returns>a &lt; b.</returns>
            public static bool operator <(ReleaseVersion a, ReleaseVersion b)
            {
                return CompareTo(a, b) < 0;
            }

            /// <summary>
            /// Returns <see cref="CompareTo"/> &gt; 0.
            /// </summary>
            /// <param name="a">Left operand.</param>
            /// <param name="b">Right operand.</param>
            /// <returns>a &gt; b.</returns>
            public static bool operator >(ReleaseVersion a, ReleaseVersion b)
            {
                return CompareTo(a, b) > 0;
            }

            /// <summary>
            /// Returns <see cref="CompareTo"/> == 0.
            /// </summary>
            /// <param name="a">Left operand.</param>
            /// <param name="b">Right operand.</param>
            /// <returns>a == b.</returns>
            public static bool operator ==(ReleaseVersion a, ReleaseVersion b)
            {
                return CompareTo(a, b) == 0;
            }

            /// <summary>
            /// Returns <see cref="CompareTo"/> != 0.
            /// </summary>
            /// <param name="a">Left operand.</param>
            /// <param name="b">Right operand.</param>
            /// <returns>a != b.</returns>
            public static bool operator !=(ReleaseVersion a, ReleaseVersion b)
            {
                return CompareTo(a, b) != 0;
            }

            /// <summary>
            /// Semantically compares 2 <see cref="ReleaseVersion"/>s.
            /// </summary>
            /// <param name="a">Left operand.</param>
            /// <param name="b">Right operand.</param>
            /// <returns>The semantic comparison.</returns>
            /// <exception cref="ArgumentException">Thrown if versions have difference prefixes.</exception>
            public static int CompareTo(ReleaseVersion a, ReleaseVersion b)
            {
                // following commented out after Microsoft changed their naming scheme starting with .NET 5
                /*
                if (!a._prefix.Equals(b._prefix))
                {
                    throw new ArgumentException($"Cannot compare versions with different prefixes: '{a}', '{b}'");
                }
                */

                return a._version.CompareTo(b._version);
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((ReleaseVersion)obj);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return _representation != null ? _representation.GetHashCode() : 0;
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return _representation;
            }

            /// <summary>
            /// Return this == other.
            /// </summary>
            /// <param name="other">Other version.</param>
            /// <returns>this == other.</returns>
            protected bool Equals(ReleaseVersion other)
            {
                return _representation == other._representation;
            }
        }
    }
}
