using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button startButton; // Referencia al botón
    public GameObject canvas; // Referencia al Canvas
    private string menuSceneName = "MenuInicio"; // Nombre de la escena de inicio (puedes ajustarlo si es diferente)

    void Start()
    {

        if (SceneManager.GetActiveScene().name == menuSceneName)
    
        // Si tienes un botón en la UI, lo puedes configurar aquí:
        if (startButton != null)
        {
            startButton.onClick.AddListener(LoadBlockingScene); // Al hacer clic, carga la escena.
        }
        
        // Si no usas el botón, también puedes continuar con el código de teclas
        Debug.Log("Presiona cualquier tecla para comenzar...");
        
        // Activa el Canvas al iniciar la escena
        canvas.SetActive(true);
    }

    void Update()
    {
        // Si se presiona cualquier tecla, carga la escena
        if (Input.anyKeyDown && SceneManager.GetActiveScene().name == "MenuInicio")
        {
            LoadBlockingScene();
        }
        
        // Si se presiona la tecla X, desactiva el Canvas
        if (Input.GetKeyDown(KeyCode.X))
        {
            canvas.SetActive(false);
        }
    }

    void LoadBlockingScene()
    {
        // Carga la escena de bloqueo
        SceneManager.LoadScene("Blocking");
    }
}

