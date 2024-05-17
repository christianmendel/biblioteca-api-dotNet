namespace Biblioteca.Settings;

public static class TokenSettings
{
    public static string SecretKey { get; set; }
    public static int ExpiresInMinutes { get; set; }

    public static void Initialize(IConfiguration configuration)
    {
        SecretKey = configuration["TokenSettings:SecretKey"];
        ExpiresInMinutes = 60;
    }
}