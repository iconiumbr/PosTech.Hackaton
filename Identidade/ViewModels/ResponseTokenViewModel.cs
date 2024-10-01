namespace Identidade.ViewModels
{
    public class ResponseTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimViewModel> Claims { get; set; }
    }
}
