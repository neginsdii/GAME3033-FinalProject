using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndGame : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Data.endGameText;
    }

    
}
