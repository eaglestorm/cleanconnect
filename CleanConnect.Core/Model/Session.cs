using System;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Common.Model.Settings;

namespace CleanConnect.Core.Model
{
    /// <summary>
    /// A user session.
    /// </summary>
    public class Session: Base<Guid>
    {
        private readonly CleanSettings _settings;

        public Session(CleanSettings settings)
        {
            _settings = settings;
            ExpiryDate = CreatedDate.AddMinutes(_settings.SessionTimeout);
        }

        public Session(Guid id, Guid refreshToken, DateTimeOffset expiryDate, DateTimeOffset created, DateTimeOffset modified)
        :base(id,created,modified)
        {
            ExpiryDate = expiryDate;
        }
        
        /// <summary>
        /// Date and time the session will expire.
        /// </summary>
        public DateTimeOffset ExpiryDate { get; }

        public bool IsValid()
        {
            return DateTimeOffset.Now > ExpiryDate;
        }
    }
}