using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedXO : MonoBehaviour
{
    [SerializeField] private Text _textUI;
    private bool _isZero;
    
    private static List<GameObject> s_blocks = new List<GameObject>();
    private static List<RedXO> s_redXO = new List<RedXO>();

    public static int GetCountBlocks()
    {
        return s_blocks.Count;
    }

    public static void AddBlock(GameObject block)
    {
        s_blocks.Add(block);
        s_redXO.Add(block.GetComponent<RedXO>());
    }

    public static void CheckAndDelete()
    {
        int countX = 0;
        foreach (var block in s_redXO)
        {
            if (block._isZero == false)
            {
                countX++;
            }
        }

        if (countX == s_blocks.Count)
        {
            foreach (var block in s_blocks)
            {
                Destroy(block);
            }
        }
    }

    private void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        _isZero = !_isZero;
        _textUI.text = _isZero ? "O" : "X";
    }
}
