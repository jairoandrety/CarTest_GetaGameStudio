/// Jairoandrety 11/02/2021

using UnityEngine;

public class MapSection : MonoBehaviour
{
    public GameObject section;

    public void ClearSection()
    {
        if (section != null)
            Destroy(section.gameObject);
    }
}
