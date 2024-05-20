using UnityEngine;
/// <summary>
/// �ܰ� ����, ������ ����
/// </summary>
public class DataManager : MonoBehaviour
{
    DataManager instance;

    public GameLevel level { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = new DataManager();
        }

        DontDestroyOnLoad(gameObject);

        level = GameLevel.EASY;

    }
}