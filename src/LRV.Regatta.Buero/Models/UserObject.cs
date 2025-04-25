namespace LRV.Regatta.Buero.Models
{
    public class UserObject
    {
        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstNme { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Roles assigned to the user
        /// </summary>
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
