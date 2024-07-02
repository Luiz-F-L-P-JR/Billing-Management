
using System.Text.Json.Serialization;

namespace Billing.Management.Domain.Auth.Model
{
    public class UserAuth
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        /// <value></value>
        public string? Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <value></value>
        public string? Password { get; set; }
    }
}
