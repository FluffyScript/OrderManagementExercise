namespace OrdersManagement.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var compareTo = obj as Entity;
            if(compareTo == null)
            {
                return false;
            }

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
