namespace SoftDeleteOneToOne.Models
{
    public class ApplicationUser
    {
	    public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        
        //public ICollection<Client> Clients { get; set; } = null!;
    }
}