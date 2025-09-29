namespace API.Storage;

public static class Paging
{
    private const int MaxLimit = 1000;

    public static int GetActualLimit(int? limit)
    {
        return limit ?? MaxLimit;
    }
    public static int CalculateOffset(int? page, int? limit)
    {
        var actualLimit = GetActualLimit(limit);
        return ((page ?? 1) - 1) * actualLimit;
    }
}