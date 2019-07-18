namespace ImageManipulation.Effects
{
    public class BlurEffect : IEffect
    {
        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            return new Blurer(bitmap).Blur();
        }
    }
}