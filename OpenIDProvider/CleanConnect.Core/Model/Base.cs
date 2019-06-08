using System;
using CleanConnect.Common.Model.Identity;

namespace CleanConnect.Core.Model
{
    public abstract class Base<T>     
    {
        public Base()
        {
            CreatedDate = DateTimeOffset.UtcNow;
            ModifiedDate = CreatedDate;
        }
        public Base(T t, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            Id = t;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }    
        
        /// <summary>
        /// Identifier for the model.
        /// </summary>
        public T Id { get; protected set; }
        
        /// <summary>
        /// When the model was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; }
        
        /// <summary>
        /// The last time the model was modified.
        /// </summary>
        public DateTimeOffset ModifiedDate { get; }

        public void SetIdentity(T t)
        {
            if (Id == null)
            {
                //TODO: throw error or set validation.
                Id = t;
            }
        }
        
    }
}