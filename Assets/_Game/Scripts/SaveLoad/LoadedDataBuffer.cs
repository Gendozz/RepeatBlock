public class LoadedDataBuffer
{
    public Save Save { get; private set; } = null;

    public void WriteInBuffer(Save save)
    {
        Save = save;
    }
}

