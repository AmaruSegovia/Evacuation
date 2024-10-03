using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognizer : MonoBehaviour
{
    // Campos necesarios para guardar las acciones accionadas por voz.
    KeywordRecognizer recognizer;
    public NPCPositionsManager positionsManager;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start()
    {
        //Declarando pares palabra - función reconocibles.
        keywords.Add("Tortuga", CambiarATortuga);
        keywords.Add("Fila", CambiarAFila);
        keywords.Add("Juntos", CambiarAJuntos);
        keywords.Add("Abrazo", CambiarAJuntos);
        keywords.Add("Cuadrado", CambiarACuadrado);
        keywords.Add("Deténganse", DetenerAliados);
        keywords.Add("Alto", DetenerAliados);
        keywords.Add("Paren", DetenerAliados);

        recognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        recognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        //Iniciando reconocedor de voz.
        recognizer.Start();
        Debug.Log("Reconocedor de voz iniciado!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Manejador de reconocimiento de palabras
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void CambiarATortuga()
    {
        positionsManager.posicionActual = Posiciones.TORTUGA;
        positionsManager.ActualizarPosicionesNPCs();
    }
    void CambiarAFila()
    {
        positionsManager.posicionActual = Posiciones.FILA;
        positionsManager.ActualizarPosicionesNPCs();
    }
    void CambiarAJuntos()
    {
        positionsManager.posicionActual = Posiciones.JUNTOS;
        positionsManager.ActualizarPosicionesNPCs();
    }
    void CambiarACuadrado()
    {
        positionsManager.posicionActual = Posiciones.CUADRADO;
        positionsManager.ActualizarPosicionesNPCs();
    }
    void DetenerAliados()
    {
        positionsManager.posicionActual = Posiciones.DETENERSE;
        positionsManager.ActualizarPosicionesNPCs();
    }


}
