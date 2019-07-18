namespace ImageManipulation.Effects
{
    public class GreyscaleEffect : IEffect
    {
        public EditableBitmap Apply(CopyableBitmap bitmap)
        {
            return new Greyalizer(bitmap).Greyalize();
        }
    }
}