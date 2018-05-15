namespace MarmitoAPI.Models
{
    public class MitoUser
    {
        public MitoUser(Mito m, User u)
        {
            Mito = m;
            User = u;
        }
        public Mito Mito { get; set; }
        public User User { get; set; }
    }
}