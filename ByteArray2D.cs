namespace FineGameDesign.Utils
{
    public sealed class ByteArray2D
    {
        public readonly byte[] bytes;
        public readonly int width;
        public readonly int height;

        public ByteArray2D(byte[] bytes, int width, int height)
        {
            this.bytes = bytes;
            this.width = width;
            this.height = height;
        }
    }
}
