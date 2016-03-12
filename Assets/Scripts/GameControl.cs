using UnityEngine;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public static GameControl control;
    public GameState state;
    public string mode;
    public Actor myCharacter;
    public Participant myself;
    public GameObject myModel;
    public bool isMyTurn;
    public int numMarkers;
    public GameObject notificationText;

    public List<GameObject> models;
    public List<string> model_names;
    public Dictionary<string, int> model_lookup;
    public Dictionary<int, string> rev_model_lookup;
    public List<Actor> actors;
    public List<FrameMarkerController> frame_markers;
    public GameObject highlighted;
    public GameObject healthbar;
    public ParticleSystem explode;

    AudioSource audioSourceFX;
    AudioSource audioSourceOverWorld;
    public AudioClip overworld;

    /*  Multiplayer Values*/
    RealTimeMultiplayerListener listener;
    WaitForState waitForState;

    public string getDM()
    {
        return state.dm;
    }

    public Actor getPlayerModels(string playerID)
    {
        return control.state.players[playerID];
    }

    public Actor getActor(int actor)
    {
        return state.frame_markers[actor];
    }

    public void setMode(string newMode)
    {
        mode = newMode;
    }

    public void setValues(GameState new_state)
    {
        state = new_state;
    }

    public void dealDamage(int actor, int damage)
    {
        if (getActor(actor).maxHealth > 0)
        {
            getActor(actor).currentHealth -= damage;
        }
    }

    public void updateMarker(int frameMarker, string model)
    {
        state.frame_markers[frameMarker] = new Actor(actors[model_lookup[model]]);
        if (!Application.isEditor)
        {
            if (mode == "Master")
            {
                PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));
            }
            else
            {
                state.currentTurnPlayer = getDM();
                PlayGamesPlatform.Instance.RealTime.SendMessage(true, getDM(), ObjectToByteArray(new MessageToDM("takenTurn", control.state, control.myself.ParticipantId)));
            }
        }
    }

    public void updateMarker(int frameMarker, Actor actor)
    {
        state.frame_markers[frameMarker] = actor;
        if (mode == "Master")
        {
            PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));
        }
        else
        {
            state.currentTurnPlayer = getDM();
            PlayGamesPlatform.Instance.RealTime.SendMessage(true, getDM(), ObjectToByteArray(new MessageToDM("takenTurn", control.state, control.myself.ParticipantId)));
        }

    }

    public Dictionary<string, string> GetPlayers()
    {
        Dictionary<string, string> players = new Dictionary<string, string>();
        if (Application.isEditor)
        {
            return players;
        }
        foreach (Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants())
        {
            if (p.ParticipantId != state.dm)
            {
                players.Add(p.Player.userName, p.ParticipantId);
                Debug.Log("Play Unity adding player " + p.Player.userName);
            }
        }
        return players;
    }

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSourceFX = gameObject.AddComponent<AudioSource>();
        audioSourceOverWorld = gameObject.AddComponent<AudioSource>();

        if (overworld != null)
        {
            audioSourceOverWorld.loop = true;
            audioSourceOverWorld.clip = overworld;
            audioSourceOverWorld.Play();
        }

        frame_markers = new List<FrameMarkerController>();
        listener = new RTMPListener();
        model_lookup = new Dictionary<string, int>();
        rev_model_lookup = new Dictionary<int, string>();
        for(int i = 0; i < model_names.Count; i++)
        {
            model_lookup.Add(model_names[i], i);
            rev_model_lookup.Add(i, model_names[i]);
        }

        numMarkers = 31;

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

        // registers a callback for turn based match notifications.
        .WithInvitationDelegate(OnInvitationReceived)

        .Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("logged in properly");
            }
            else
            {
                Debug.Log("did not log in properly");
            }
        });
    }

    public void playFXSound(AudioClip sound)
    {
        audioSourceFX.PlayOneShot(sound);
    }

    void Update()
    {
    }

    public void PopupMessage(string message)
    {
        /*notificationText = GameObject.FindWithTag("Notification");
        Text text = notificationText.GetComponent<Text>();
        text.color = Color.white;
        text.text = message;
        WaitAndClear();*/
    }

    IEnumerator WaitAndClear()
    {
        yield return new WaitForSeconds(1);
        notificationText = GameObject.FindWithTag("Notification");
        Text text = notificationText.GetComponent<Text>();
        text.text = "";
    }

    /******************************************************
     *      Multiplayer Functions                         *
     ******************************************************/

    public delegate void WaitForState();

    public void OnInvitationReceived(Invitation invitation, bool shouldAutoLaunch)
    {

    }

    public void CreateWithInvitationScreen()
    {
        if (Application.isEditor)
        {
            state = new GameState();
            state.dm = "meee";
            state.frame_markers = new List<Actor>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new Actor(0, state.dm));
            }
            SceneManager.LoadScene(1);
        }
        else
        {
            const int MinPlayers = 1;
            const int MaxPlayers = 7;
            const int Variant = 0;  // default
            PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinPlayers, MaxPlayers, Variant, listener);
        }
    }

    public void AcceptFromInbox()
    {
        if (Application.isEditor)
        {
            state = new GameState();
            state.dm = "meee";
            state.frame_markers = new List<Actor>();
            for (int i = 0; i < numMarkers; i++)
            {
                state.frame_markers.Add(new Actor(0, state.dm));
            }
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(listener);
        }
    }

    public void TakeTurn(string next)
    {
        Debug.Log("Take Turn was called");
        if (control.mode == "Player")
        {
            if (isMyTurn)
            {
                Debug.Log("sending player turn");
                state.currentTurnPlayer = state.dm;
                PlayGamesPlatform.Instance.RealTime.SendMessage(true, control.state.dm, ObjectToByteArray(new MessageToDM("takenTurn", control.state, control.myself.ParticipantId)));
            }
            else
            {
                Debug.Log("Not my turn play");
            }
        }
        if (control.mode == "Master")
        {
            Debug.Log("Sending DM Updates");
            state.currentTurnPlayer = next;
            PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));
        }
    }

    public static byte[] ObjectToByteArray(System.Object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    class RTMPListener : RealTimeMultiplayerListener
    {

        private bool showingWaitingRoom = false;
        /// <summary>
        /// Called during room setup to notify of room setup progress.
        /// </summary>
        /// <param name="percent">The room setup progress in percent (0.0 to 100.0).</param>
        public void OnRoomSetupProgress(float progress)
        {
            // show the default waiting room.
            if (!showingWaitingRoom)
            {
                showingWaitingRoom = true;
                PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
            }
        }

        /// <summary>
        /// Notifies that room setup is finished. If <c>success == true</c>, you should
        /// react by starting to play the game; otherwise, show an error screen.
        /// </summary>
        /// <param name="success">Whether setup was successful.</param>
        public void OnRoomConnected(bool success)
        {
            if (success)
            {
                control.myself = PlayGamesPlatform.Instance.RealTime.GetSelf();

                if (control.mode == "Master")
                {
                    control.state = new GameState();
                    control.state.dm = control.myself.ParticipantId;
                    control.state.frame_markers = new List<Actor>();
                    control.state.players = new Dictionary<string, Actor>();
                    foreach (Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants())
                    {
                        if (p.ParticipantId != control.state.dm)
                        {
                            control.state.players.Add(p.ParticipantId, new Actor());
                            Debug.Log("Play Unity adding player " + p.Player.userName);
                        }
                    }
                    for (int i = 0; i < control.numMarkers; i++)
                    {
                        control.state.frame_markers.Add(new Actor(0, control.state.dm));
                    }
                    PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));

                    if (SceneManager.GetActiveScene().buildIndex < 1)
                    {
                        SceneManager.LoadScene(1);
                    }
                }

                if (control.mode == "Player")
                {
                    control.waitForState = new WaitForState(loadMain);
                }
            }
            else
            {
                Debug.Log("Unity Failed to connect to the room");
            }
        }

        public void loadMain()
        {
            PlayGamesPlatform.Instance.RealTime.SendMessage(true, control.state.dm, ObjectToByteArray(new MessageToDM("characterUpdate", control.myCharacter, control.myself.ParticipantId)));
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Notifies that the current player has left the room. This may have happened
        /// because you called LeaveRoom, or because an error occurred and the player
        /// was dropped from the room. You should react by stopping your game and
        /// possibly showing an error screen (unless leaving the room was the player's
        /// request, naturally).
        /// </summary>
        public void OnLeftRoom()
        {

        }

        /// <summary>
        /// Raises the participant left event.
        /// This is called during room setup if a player declines an invitation
        /// or leaves.  The status of the participant can be inspected to determine
        /// the reason.  If all players have left, the room is closed automatically.
        /// </summary>
        /// <param name="participant">Participant that left</param>
        public void OnParticipantLeft(Participant participant)
        {

        }

        /// <summary>
        /// Called when peers connect to the room.
        /// </summary>
        /// <param name="participantIds">Participant identifiers.</param>
        public void OnPeersConnected(string[] participantIds)
        {

        }

        /// <summary>
        /// Called when peers disconnect from the room.
        /// </summary>
        /// <param name="participantIds">Participant identifiers.</param>
        public void OnPeersDisconnected(string[] participantIds)
        {

        }

        /// <summary>
        /// Called when a real-time message is received.
        /// </summary>
        /// <param name="isReliable">Whether the message was sent as a reliable message or not.</param>
        /// <param name="senderId">Sender identifier.</param>
        /// <param name="data">Data.</param>
        public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
        {
            Debug.Log("unity DM got new message");
            if (control.mode == "Master")
            {
                MessageToDM message = (MessageToDM)ByteArrayToObject(data);
                switch(message.messageType)
                {
                    case("characterUpdate"):
                        Debug.Log("Message was character update");
                        control.state.players[message.myID] = message.myCharacter;
                        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));
                        break;
                    case ("takenTurn"):
                        Debug.Log("Message was taking a turn");
                        control.state = message.state;
                        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, ObjectToByteArray(control.state));
                        break;
                    default:
                        break;
                }
            }

            if (control.mode == "Player")
            {
                Debug.Log("unity player got new message, setting values");
                control.setValues((GameState)ByteArrayToObject(data));
                if(control.state.currentTurnPlayer == control.myself.ParticipantId)
                {
                    control.isMyTurn = true;
                }
                else
                {
                    control.isMyTurn = false;
                }
                if (control.waitForState != null)
                {
                    control.waitForState();
                    control.waitForState = null;
                }
            }
            
        }

        public static System.Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}

[System.Serializable]
public class GameState
{
    public string currentTurnPlayer;
    public string dm;
    public List<Actor> frame_markers;
    public Dictionary<string, Actor> players;
}

[System.Serializable]
public class Actor
{
    public Actor()
    {
        inventory = new List<string>();
    }

    public Actor(int new_model, string new_player)
    {
        model = new_model;
        player = new_player;
        inventory = new List<string>();
    }

    public Actor(Actor old_actor)
    {
        isPlayer = old_actor.isPlayer;
        model = old_actor.model;
        player = GameControl.control.getDM();
        characterName = old_actor.characterName;
        characterClass = old_actor.characterClass;
        characterRace = old_actor.characterRace;
        str = old_actor.str;
        dex = old_actor.dex;
        con = old_actor.con;
        intelligence = old_actor.intelligence;
        wiz = old_actor.wiz;
        cha = old_actor.cha;
        maxHealth = old_actor.maxHealth;
        currentHealth = old_actor.currentHealth;
        range = old_actor.range;
        chestItem = old_actor.chestItem;
        rightHandWeapon = old_actor.rightHandWeapon;
        leftHandWeapon = old_actor.leftHandWeapon;
        chestArmor = old_actor.chestArmor;
        inventory = new List<string>(old_actor.inventory);
    }

    public bool isPlayer;
    public int model;
    public string player;
    public string characterName;
    public string characterClass;
    public string characterRace;
    public int str, dex, con, intelligence, wiz, cha;
    public int maxHealth;
    public int currentHealth;
    public int range;
    public string chestItem;
    public string rightHandWeapon;
    public string leftHandWeapon;
    public string chestArmor;
    public List<string> inventory;
}

[System.Serializable]
public class MessageToDM
{
    public MessageToDM(string messageTypeIn, Actor myCharacterIn, string ID)
    {
        messageType = messageTypeIn;
        myCharacter = myCharacterIn;
        myID = ID;
    }
    public MessageToDM(string messageTypeIn, GameState newState, string ID)
    {
        messageType = messageTypeIn;
        state = newState;
        myID = ID;
    }
    public string myID;
    public string messageType;
    public Actor myCharacter;
    public GameState state;
}