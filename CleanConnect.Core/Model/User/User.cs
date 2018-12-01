namespace CleanConnect.Core.Model.User
{
    /// <summary>
    /// A user of the authorization server.
    /// </summary>
    public class User
    {

        public User(Credential credential, PersonalDetails personalDetails, ContactDetails contactDetails)
        {
            Credential = credential;
            PersonalDetails = personalDetails;
            ContactDetails = contactDetails;
        }
        
        /// <summary>
        /// The users authentication credentials.
        /// </summary>
        public Credential Credential { get; }
        
        /// <summary>
        /// The users personal details like name and date of birth.
        /// </summary>
        public PersonalDetails PersonalDetails { get; }

        /// <summary>
        /// The users contact details.
        /// </summary>
        public ContactDetails ContactDetails { get; }
    }
}