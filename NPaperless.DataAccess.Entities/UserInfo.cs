using System.Runtime.Serialization;

namespace NPaperless.DataAccess.Entities
{
    public class UserInfo
    {
        public long Id { get; set; }
        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }
    }
}
