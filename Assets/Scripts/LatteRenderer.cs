using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatteRenderer : MonoBehaviour {

    private const Color32 c_coffeeColour = new Color32(75, 55, 30, 255);
    private const Color32 c_milkColour = new Color32(240, 230, 190, 255);

    public Texture2D texture;

    public void RenderLatte(byte[,] array) {
        ClearTexture();
        int maxX = array.GetLength(0) - 1, maxY = array.GetLength(1) - 1;
        for (int x = 0; x <= maxX; x++) {
            for (int y = 0; y <= maxY; y++) {
                DrawCell(x, y, maxX, maxY, array[x, y]);
            }
        }
        texture.Apply();
    }

    private void ClearTexture() {
        Color32[] resetColorArray = texture.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++) {
            resetColorArray[i] = c_coffeeColour;
        }

        texture.SetPixels32(resetColorArray);
    }

    private int TextureTransform(int val, int maxVal, int textureVal) {
        return val * textureVal / maxVal;
    }

    private void DrawCell(int x, int y, int maxX, int maxY, byte intensity) {
        int cX = TextureTransform(x, maxX, texture.width), cY = TextureTransform(y, maxY, texture.height);
        DrawCircle(c_milkColour, cX, cY, intensity);
    }

    private void DrawCircle(Color32 color, int x, int y, byte radius) {
        for (int u = x - radius; u <= x + radius; u++) {
            for (int v = y - radius; v <= y + radius; v++) {
                if ((x - u) * (x - u) + (y - v) * (y - v) < radius * radius) {
                    texture.SetPixel(u, v, color);
                }
            }
        }
    }
}
