using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RandomBag
{
  private int size;
  private int index;
  private List<int> m_bag;

  public RandomBag(int n)
  {
    size = n;
    m_bag = new List<int>();
    for (int i = 0; i < size; ++i) m_bag.Add(i);
    shuffle();
  }

  private void shuffle()
  {
    // Keep track of current last to avoid repetition after shuffle
    int last = m_bag[size - 1];
    for (int i = 0; i < size - 1; ++i)
    {
      // get random index
      int nextIndex = Random.Range(i, size);

      // swap it with position i
      if (nextIndex == i) continue;
      var tmp = m_bag[nextIndex];
      m_bag[nextIndex] = m_bag[i];
      m_bag[i] = tmp;
    }

    // If we would repeat the last symbol from previous bag,
    // swap it with the last one
    if (last == m_bag[0])
    {
      var tmp = m_bag[size - 1];
      m_bag[size - 1] = m_bag[0];
      m_bag[0] = tmp;
    }
    index = 0;
  }

  public int get()
  {
    if (index == size) shuffle();
    var res = m_bag[index];
    ++index;
    return res;
  }
}

public class ShapeManager : MonoBehaviour
{
  private Sprite[] m_sprites = new Sprite[0];
  private int m_currentSpriteIndex = 0;
  private SpriteRenderer m_spriteRenderer;
  private RandomBag m_randomBag;

  // Start is called before the first frame update
  void Start()
  {
    m_sprites = Resources.LoadAll<Sprite>("Shapes");
    m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    m_randomBag = new RandomBag(m_sprites.Length);
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
      index = m_randomBag.get();
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
