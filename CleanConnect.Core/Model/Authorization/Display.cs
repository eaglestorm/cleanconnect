using CleanConnect.Common.Model;

namespace CleanConnect.Core.Model
{
    /// <summary>
    /// The requested way to display the login page.
    /// </summary>
    public sealed class Display: TypeSafeEnum
    {
        /// <summary>
        /// Page in the browser.
        /// </summary>
        public static readonly Display Page = new Display(1, "page");
        
        /// <summary>
        /// New popup window usually smaller than the parent so as not to obscure it. 
        /// </summary>
        public static readonly Display Popup = new Display(2, "popup");
        
        /// <summary>
        /// Touch based interface.
        /// </summary>
        public static readonly Display Touch = new Display(1, "touch");
        
        /// <summary>
        /// Feature phone based interface.
        /// </summary>
        public static readonly Display Wap = new Display(1, "wap");
        
        private Display(int value, string name) : base(value, name)
        {
            
        }
    }
}