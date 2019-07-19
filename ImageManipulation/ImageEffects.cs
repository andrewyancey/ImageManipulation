namespace ImageManipulation
{
    using ImageManipulation.Effects;

    public static class ImageEffects
    {
        public static EditableBitmap Blur(CopyableBitmap inputImage)
        {
            return EffectFactory.CreateEffect(EffectType.Blur)
                                .Apply(inputImage);
        }

        public static EditableBitmap Greyalize(CopyableBitmap inputImage)
        {
            return EffectFactory.CreateEffect(EffectType.Grayscale)
                                .Apply(inputImage);
        }

        public static EditableBitmap Pixelize(CopyableBitmap inputImage)
        {
            return EffectFactory.CreateEffect(EffectType.Pixelize)
                                .Apply(inputImage);
        }

        public static EditableBitmap Redscale(CopyableBitmap inputImage)
        {
            return EffectFactory.CreateEffect(EffectType.Redscale)
                                .Apply(inputImage);
        }
    }
}