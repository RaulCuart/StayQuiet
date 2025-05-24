using UnityEngine;

public class DoorCombination : MonoBehaviour
{
    public Button[] buttons;
    public bool[] combination;
    public GameObject door;

    void Update()
    {
        if (checkCombination())
        {
            door.SetActive(false);
        }
    }

    public bool checkCombination()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (combination[i] != buttons[i].activated)
            {
                return false;
            }
        }
        return true;
    }
}
