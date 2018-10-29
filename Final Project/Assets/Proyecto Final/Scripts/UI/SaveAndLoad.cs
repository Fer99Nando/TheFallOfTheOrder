using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{

    GameObject Player;

	// Use this for initialization


    public void SaveGame()
    {
        PlayerPrefs.SetFloat("Player x", Player.transform.position.x);
        PlayerPrefs.SetFloat("Player y", Player.transform.position.z);
        PlayerPrefs.SetFloat("Player z", Player.transform.position.z);

    }

    public void LoadPosition()
    {
        SceneManager.LoadScene("Gameplay");

        float x = PlayerPrefs.GetFloat("Player x");
        float y = PlayerPrefs.GetFloat("Player y");
        float z = PlayerPrefs.GetFloat("Player z");

        transform.position = new Vector3(x, y, z);

    }

    public void DeleteGame()
    {
        PlayerPrefs.DeleteKey("Player x");
        PlayerPrefs.DeleteKey("Player z");
        PlayerPrefs.SetFloat("Level1", Player.GetComponent<PlayerControl>().forwardSpeed);
    }
}
