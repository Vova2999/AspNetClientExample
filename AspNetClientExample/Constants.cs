namespace AspNetClientExample;

public static class Constants
{
    public const string ServerAddress = "http://localhost:33203/";
    public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(30);
    public const int RefreshSlidingTokenBeforeExpirationInPercent = 40;
}