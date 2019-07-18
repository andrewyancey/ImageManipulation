namespace ImageManipulation.Effects
{
    public class PixelizeEffect : IEffect
    {
        private readonly int _spacingX;

        private readonly int _spacingY;

        public PixelizeEffect(int spacingX, int spacingY)
        {
            _spacingX = spacingX;
            _spacingY = spacingY;
        }

        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            return new Pixelizer(bitmap, _spacingX, _spacingY).Pixelize();
        }
    }
}