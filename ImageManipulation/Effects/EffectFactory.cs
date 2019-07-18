namespace ImageManipulation.Effects
{
    using System;

    public static class EffectFactory
    {
        public static IEffect CreateEffect(EffectType effectType)
        {
            switch (effectType)
            {
                case EffectType.Blur:
                    return new BlurEffect();

                case EffectType.Grayscale:
                    return  new GreyscaleEffect();

                case EffectType.Pixelize:
                    return new PixelizeEffect(5, 5);

                default:
                    throw new ArgumentOutOfRangeException(nameof(effectType), effectType, "Invalid effect type.");
            }
        }
    }
}