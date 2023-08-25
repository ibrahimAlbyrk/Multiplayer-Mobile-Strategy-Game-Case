namespace Core.Runtime.Units.Types
{
    /// <summary>
    /// Command types sent for the unit's task selection.
    /// <para>Included Commands:</para>
    /// <param name="Move"></param>
    /// <param name="Collect"></param>
    /// </summary>
    public enum Unit_CommandType
    {
        MoveToVector,
        MoveToTransform,
        ResourceCollect
    }
}