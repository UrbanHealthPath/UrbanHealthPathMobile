using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Scalers
{
    public class TextureRatioScaler
    {
        public static Texture2D GetScaledTexture(RectTransform maxSize, Texture2D texture)
        {
            var maxWidth = maxSize.rect.width;
            var maxHeight = maxSize.rect.height;

            var textureWidth = texture.width;
            var texturHeight = texture.height;
            
            var ratio = textureWidth / texturHeight;

            float targetWidth = textureWidth, targetHeight = texturHeight;

            do
            {
                if (maxWidth < targetWidth)
                {
                    targetWidth = maxWidth;
                    targetHeight = targetWidth / ratio;
                }

                if (maxHeight < targetHeight)
                {
                    targetHeight = maxHeight;
                    targetWidth = ratio * targetHeight;
                }
            } while (targetHeight <= maxHeight && targetWidth <= maxWidth);

            return (Mathf.Approximately(targetHeight, texturHeight) && Mathf.Approximately(targetWidth ,textureWidth))
                ? texture
                : TextureScaler.Scaled(texture, (int)targetWidth, (int)targetHeight);
        }
    }
}
