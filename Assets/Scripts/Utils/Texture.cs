using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class TextureUtils
    {
        public static byte[] GetOpacityArray(Texture2D texture)
        {
            var pixels = texture.GetPixels32();
            var opacityArray = new byte[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                opacityArray[i] = pixels[i].a;
            };
            return opacityArray;
        }
    }
}