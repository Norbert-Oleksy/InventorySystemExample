using System.Collections;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Start()
    {
        TestManager.Instance.OnGameStart += () => StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
