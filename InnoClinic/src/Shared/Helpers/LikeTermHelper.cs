namespace InnoClinic.Shared.Helpers
{
    public static class LikeTermHelper
    {
        public static string EscapeLikeTerm(string term) =>
            term.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
    }
}
