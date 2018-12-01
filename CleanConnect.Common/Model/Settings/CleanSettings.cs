namespace CleanConnect.Common.Model.Settings
{
    /// <summary>
    /// Settings for clean connect.
    /// </summary>
    public class CleanSettings
    {
        public CleanSettings()
        {
            DbSettings = new DbSettings();
        }
        
        
        
        public DbSettings DbSettings { get; set; }
        
        public int SessionTimeout { get; set; }
    }
}