namespace ImageManipulation.Effects
{
    public interface IEffect
    {
        EditableBitmap Apply(CopyableBitmap bitmap);
    }
}