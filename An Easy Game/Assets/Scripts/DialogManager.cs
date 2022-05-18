using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    private Queue<string> lines;
    private bool typeEnd = false;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        lines = new Queue<string>();
    }

    private void Update()
    {
        ShowNextLine();
    }

    public void StartDialog(Dialog dialog)
    {
        lines.Clear();
        foreach (string line in dialog.lines)
        {
            lines.Enqueue(line);
        }
        dialogBox.SetActive(true);
        dialogText.text = "";
        StartCoroutine(TypeDialog(lines.Dequeue()));
    }

    public void ShowNextLine()
    {
        // If there's no more line, disable the dialogBox show
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lines.Count == 0)
            {
                dialogBox.SetActive(false);
                return;
            }
            string line = lines.Peek();
            StartCoroutine(TypeDialog(line));
            lines.Dequeue();
        }
    }

    // Show dialog text letter by letter
    public IEnumerator TypeDialog(string line)
    {
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 20);
        }
        typeEnd = true;
    }
}
