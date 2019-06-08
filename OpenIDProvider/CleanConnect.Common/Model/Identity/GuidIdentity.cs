using System;

namespace CleanConnect.Common.Model.Identity
{
    public class GuidIdentity: IDbIdentity<Guid?>
    {
        public GuidIdentity()
        {
            Id = Guid.NewGuid();
        }
        public Guid? Id { get; private set; }
        public void SetIdentity(Guid? t)
        {
            if (Id.HasValue)
            {
                //safety check.
                throw new ArgumentException("The Id already has a value.");
            }

            Id = t;
        }
    }
}