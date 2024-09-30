using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognizer : MonoBehaviour
{
    // Campos necesarios para guardar las acciones accionadas por voz.
    KeywordRecognizer recognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start()
    {
        //Declarando pares palabra - función reconocibles.
        keywords.Add("Hola", ReconocerHola);

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

    void ReconocerHola()
    {
        Debug.Log("Holaaa :D");
    }

}
