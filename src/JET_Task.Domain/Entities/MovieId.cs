using System;
using System.Text.RegularExpressions;

namespace JET_Task.Domain.Entities
{
    public sealed class MovieId : IEquatable<MovieId>
    {
        private static readonly Regex ImdbIdRegex = new Regex(@"^tt\d{7,8}$", RegexOptions.Compiled);
        public string Value { get; }

        public MovieId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("MovieId cannot be empty.", nameof(value));
            if (!ImdbIdRegex.IsMatch(value))
                throw new ArgumentException("Invalid IMDb ID format.", nameof(value));
            Value = value;
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => Equals(obj as MovieId);
        public bool Equals(MovieId? other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public static implicit operator string(MovieId id) => id.Value;
    }
} 