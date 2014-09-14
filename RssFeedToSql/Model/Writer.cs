namespace RssFeedToSql.Model
{
    public class Writer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        protected bool Equals(Writer other)
        {
            return string.Equals(Email.ToUpperInvariant().Trim(), other.Email.ToUpperInvariant().Trim()) &&
                   string.Equals(Name.ToUpperInvariant().Trim(), other.Name.ToUpperInvariant().Trim());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Email != null ? Email.GetHashCode() : 0)*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Writer) obj);
        }
    }
}