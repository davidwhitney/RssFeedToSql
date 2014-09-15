namespace RssFeedToSql.Model
{
    public class Publication : IHaveAnId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected bool Equals(Publication other)
        {
            return string.Equals(Name.ToUpperInvariant(), other.Name.ToUpperInvariant());
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}