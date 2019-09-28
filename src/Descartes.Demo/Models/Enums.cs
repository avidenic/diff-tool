namespace Descartes.Demo.Models
{
    /// <summary>
    /// Diff side
    /// </summary>
    public enum Side
    {
        Left = 0,
        Right = 1
    }

    /// <summary>
    /// Result types when trying to calculate a diff
    /// </summary>
    public enum DiffResult
    {
        Equals,
        ContentDoesNotMatch,
        SizeDoesNotMatch
    }
}