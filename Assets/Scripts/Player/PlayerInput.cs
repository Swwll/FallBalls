using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    private bool _locked;

    public bool HitButtonDown { get; set; }

    private void Update()
    {
        if (_locked == false)
            HitButtonDown = Input.GetMouseButtonDown(0);
    }

    public void Lock()
    {
        HitButtonDown = false;
        _locked = true;
    }

    public void Unlock()
    {
        _locked = false;
    }
}