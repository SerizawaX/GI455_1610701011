using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finder : MonoBehaviour
{
    public List<string> words;
    public Text listBox;
    public Text resultBox;
    public InputField inputBox;

    // Start is called before the first frame update
    void Start()
    {
        string list = "";
        for (int i=0; i< words.Count; i++)
            list += words[i] + "\n";
        listBox.text = list;
    }

    public void FindText()
    {
        if (words.Contains(inputBox.text))
            resultBox.text = "<color=green> " + inputBox.text + " </color> is found.";
        else
            resultBox.text = "<color=red> " + inputBox.text + " </color> is not found.";
    }

}
