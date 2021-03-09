using System.Collections.Generic;

namespace SoftDeleteOneToOne.Models
{
    public class ApplicationUser
    {
	    public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        // If this navigation back to Client is commented out, the SoftDelete throws an error
        // "You cannot soft delete a one-to-one relationship. It causes problems if you try to create a new version."
        public ICollection<Client> Clients { get; set; } = null!;
    }
}