using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private BoolVariableSO _pauseVariable;
    
    [SerializeField, ReadOnly]
    private bool _isInitialized = false;

    private void Awake() 
    {
        Event_InitializeManager(null);    
    }

    public void Event_InitializeManager(object eventData)
    {
        _isInitialized = true;
    }

    private void Update() 
    {
        if (!_isInitialized)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseVariable.RuntimeValue = !_pauseVariable.RuntimeValue;
        }
    }    
}