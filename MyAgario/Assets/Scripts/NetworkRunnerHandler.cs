using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunnerPrefab; // Reference to the runner prefab
    private NetworkRunner networkRunner;

    private void Awake()
    {
        // Find if there's already a NetworkRunner in the scene
        NetworkRunner runnerInScene = FindObjectOfType<NetworkRunner>();

        if (runnerInScene != null)
        {
            networkRunner = runnerInScene;
        }
    }

    private void Start()
    {
        if (networkRunner == null)
        {
            // Instantiate a new NetworkRunner if one isn't found
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network Runner";

            GameMode gameMode = GameMode.Client; // Default to client

#if UNITY_EDITOR
            gameMode = GameMode.AutoHostOrClient; // In the editor, auto-host if no host exists
#elif UNITY_SERVER
            gameMode = GameMode.Server; // Dedicated server mode
#endif
            // Get the active scene's build index as a SceneRef
            SceneRef activeSceneRef = SceneManager.GetActiveScene().buildIndex;

            // Initialize the NetworkRunner
            InitializeNetworkRunner(networkRunner, gameMode, "TestSession", NetAddress.Any(), activeSceneRef);
        }
    }

    // Initializes the NetworkRunner with the given parameters
    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName, 
        NetAddress address, SceneRef scene)
    {
        // Set up the scene manager
        INetworkSceneManager sceneManager = GetSceneManager(runner);

        // Enable input provision if needed
        runner.ProvideInput = true;

        // Start the game with the provided arguments
        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            SceneManager = sceneManager
        });
    }

    // Helper method to get or add a scene manager
    private INetworkSceneManager GetSceneManager(NetworkRunner runner)
    {
        var sceneManager = runner.GetComponent<MonoBehaviour>() as INetworkSceneManager;

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        return sceneManager;
    }

    // Optional: Handle player joining events
    private void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player.PlayerId} has joined the game.");
        // Spawn player prefabs, initialize player data, etc.
    }

    // Optional: Handle session state changes
    private void OnSessionStateChanged(NetworkRunner runner, NetworkRunner.SessionStateChangedArgs args)
    {
        Debug.Log($"Session state changed to {args.State}");
        // Handle state changes, e.g., transitioning to gameplay, waiting for players, etc.
    }
}
