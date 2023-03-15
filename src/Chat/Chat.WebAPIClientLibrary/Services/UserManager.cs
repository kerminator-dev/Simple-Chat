namespace Chat.WebAPIClientLibrary.Services
{
    internal class UserManager : IUserManager
    {
        protected readonly string _deleteUserRoute;

        public UserManager(Uri host)
        {
            // Роуты к API-методам
            _deleteUserRoute = $"{host}api/User/Delete";
        }

        public Task<bool> TryDeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
