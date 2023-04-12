using System.ComponentModel.DataAnnotations;

namespace LocalStack.Api.Contracts.Requests
{
    public class CreateAccountRequestV1
    {
        /// <summary>
        /// Gets or sets Account Name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
