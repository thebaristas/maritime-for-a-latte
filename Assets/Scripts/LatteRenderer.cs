using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatteRenderer : MonoBehaviour {

    public Texture2D texture;

    private readonly Color32 c_backgroundColour = new Color32(0, 0, 0, 0);
    private readonly Color32 c_milkColour = new Color32(240, 230, 190, 240);

    public void RenderLatte(byte[,] array)
    {
        ClearTexture();
        int maxX = array.GetLength(0) - 1, maxY = array.GetLength(1) - 1;
        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                DrawCell(x, y, maxX, maxY, array[x, y]);
            }
        }
        texture.Apply();
    }

    public void ClearTexture()
    {
        Color32[] resetColourArray = texture.GetPixels32();

        for (int i = 0; i < resetColourArray.Length; i++)
        {
            resetColourArray[i] = c_backgroundColour;
        }

        texture.SetPixels32(resetColourArray);
    }

    private int TextureTransform(int val, int maxVal, int textureVal)
    {
        return val * textureVal / maxVal;
    }

    private int ComputeRadius(byte intensity)
    {
        return (int) Mathf.Sqrt(GameManager.instance.gameSettings.dropSizeFactor * intensity);
    }

    private void DrawCell(int x, int y, int maxX, int maxY, byte intensity)
    {
        int cX = TextureTransform(x, maxX, texture.width), cY = TextureTransform(y, maxY, texture.height);
        if (intensity > 0)
        {
            DrawCircle(c_milkColour, cX, cY, ComputeRadius(intensity));
        }
    }

    private void DrawCircle(Color32 colour, int x, int y, int radius)
    {
        for (int u = x - radius; u <= x + radius; u++)
        {
            for (int v = y - radius; v <= y + radius; v++)
            {
                if ((x - u) * (x - u) + (y - v) * (y - v) < radius * radius)
                {
                    texture.SetPixel(u, v, colour);
                }
            }
        }
    }

    public float[] GetOpacityArray()
    {
        return Utils.TextureUtils.GetOpacityArray(texture);
    }
}
