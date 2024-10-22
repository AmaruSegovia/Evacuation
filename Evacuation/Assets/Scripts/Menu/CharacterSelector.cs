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

    void Update(){

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            AnteriorCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            SiguienteCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)){
            StartGame();
        }
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
        if(nombre.text == "Random"){
            Debug.Log("Personaje aleatorio");
        }
        //Carga la siguiente escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}


