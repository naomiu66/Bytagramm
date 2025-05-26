namespace BytagrammAPI.Services.Abstractions
{
    public interface IJwtService 
    {
        public string GenerateToken(string UserId, string UserName);
    }
}
