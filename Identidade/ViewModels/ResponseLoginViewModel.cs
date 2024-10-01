namespace Identidade.ViewModels
{
    public class ResponseLoginViewModel
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public ResponseTokenViewModel UsuarioToken { get; set; }
    }
}
