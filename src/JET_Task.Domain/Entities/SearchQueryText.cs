using System.Text.Json.Serialization;

namespace JET_Task.Domain.Entities
{
    public sealed class SearchQueryText : IEquatable<SearchQueryText>
    {
        public const int MaxLength = 100;

        [JsonInclude]
        public string Value { get; private set; }

        // Parameterless constructor for JSON deserialization
        public SearchQueryText() { }

        public SearchQueryText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Search query cannot be empty.", nameof(value));
            if (value.Length > MaxLength)
                throw new ArgumentException($"Search query cannot exceed {MaxLength} characters.", nameof(value));
            Value = value;
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => Equals(obj as SearchQueryText);
        public bool Equals(SearchQueryText? other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public static implicit operator string(SearchQueryText text) => text.Value;
    }
} 