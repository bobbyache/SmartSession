﻿namespace CygSoft.SmartSession.Infrastructure
{
    public abstract class EntityBase
    {
        public int Id { get; set; } = -1;

        protected EntityBase()
        {
        }

        public override bool Equals(object entity)
        {
            if (entity == null || !(entity is EntityBase))
            {
                return false;
            }
            return (this == (EntityBase)entity);
        }

        public static bool operator ==(EntityBase base1,
            EntityBase base2)
        {
            if ((object)base1 == null && (object)base2 == null)
            {
                return true;
            }
            if ((object)base1 == null || (object)base2 == null)
            {
                return false;
            }
            if (base1.Id != base2.Id)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(EntityBase base1,
            EntityBase base2)
        {
            return (!(base1 == base2));
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

}
