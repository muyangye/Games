using UnityEngine;

public class NPCShowDialog : MonoBehaviour
{
    [SerializeField] Dialog dialog;

    public void Show()
    {
        DialogManager.Instance.StartDialog(dialog);
    }
}
