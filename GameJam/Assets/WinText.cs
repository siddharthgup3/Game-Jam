using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinText : MonoBehaviour
{
    public bool spawn;
    public bool tracy;
    public bool anti;

    public GameObject spawnGO;
    public GameObject tracyGO;
    public GameObject antiGO;

    private AudioSource aS;


    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();

        if (spawn)
        {
            spawnGO.SetActive(true);
        }

        if (tracy)
        {
            tracyGO.SetActive(true);
        }

        if (spawn)
        {
            antiGO.SetActive(true);
        }

        StartCoroutine(PlayWinState());
    }

    public IEnumerator PlayWinState()
    {
        aS.Play();

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HubSceneUnlocked");
    }
}
