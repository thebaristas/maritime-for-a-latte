using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    private Sprite[] m_sprites = new Sprite[0];
    private int m_currentSpriteIndex = 0;
    private SpriteRenderer m_spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_sprites = Resources.LoadAll<Sprite>("Shapes");
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteRandomly()
    {
        if (m_sprites.Length == 1)
        {
            return;
        }
        int index = m_currentSpriteIndex;
        while (index == m_currentSpriteIndex)
        {
            index = Random.Range(0, m_sprites.Length - 1);
        }
        SetSprite(index);
    }

    private void SetSprite(int index)
    {
        m_currentSpriteIndex = index;
        m_spriteRenderer.sprite = m_sprites[m_currentSpriteIndex];
    }

    public byte[] GetOpacityArray()
    {
        return Utils.TextureUtils.GetOpacityArray(m_spriteRenderer.sprite.texture);
    }
}
