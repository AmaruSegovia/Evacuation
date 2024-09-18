using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI nombre;

    private int index;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        index = PlayerPrefs.GetInt("SelectedCharacterIndex");
        
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", index);
        imagen.sprite = gameManager.characters[index].imagen;
        nombre.text = gameManager.characters[index].characterName;
        description.text = gameManager.characters[index].description;
    }

    public void SiguienteCharacter()
    {
        index = (index + 1) % gameManager.characters.Count;
        UpdateCharacterDisplay();
    }
    public void AnteriorCharacter()
    {
        index = (index - 1 + gameManager.characters.Count) % gameManager.characters.Count; // Cambia el índice hacia atrás
        UpdateCharacterDisplay();
    }

    public void StartGame()
    {
        //
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}


