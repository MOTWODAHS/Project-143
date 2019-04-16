using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

namespace Singing
{
    class GameController : MonoBehaviour, IGameController
    {
        private const int GAME_CODE = 2;

        private const float PLAYBACK_INTERVAL = 0.25f;

        private const float SEND_DELAY = 5f;

        private int gameStage;

        private string songString;

        private bool playingSong = false;

        private List<AudioSource> song = new List<AudioSource>();

        private Animator cameraAnim;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        private StageTransition[] rTransitions;

        //Public fields
        public AudioSource[] audioClips;

        [Space]
        public BirdNote birdNote;

        [Header("Buttons")]
        public Transition newSong;
        public Transition done;
        public Transition playSong;
        //public Transition closeButton;

        [Header("Camera")]
        public GameObject cam;

        [Header("NotePiano")]
        public GameObject notePiano;

        [Header("Birds")]
        public GameObject birds;
        public Collider2D staff;
        public Animator grayBird;
        public AudioSource scrollOpen;
        public AudioSource scrollClose;

        [Header("ColorButtons")]
        public GameObject cButtons;

        [Header("Mask")]
        public GameObject mask;

        [Header("Default Notes")]
        public GameObject defualtNotes;

        [Header("Hand")]
        public GameObject hand;

        [Header("Background")]
        public GameObject background;

        [Header("EndButton")]
        public GameObject endUI;

        [Header("Network")]
        public NetworkingController network;

        [Header("Keyboard")]
        public GameObject keyboard;

        private void Start()
        {
            gameStage = 0;
            cameraAnim = cam.GetComponent<Animator>();
            grayBird.GetComponent<Animator>().Play("landing");
            birds.transform.DOMove(new Vector3(-3.918748f, -5.094825f, -0.03943332f), 4.4f);
            transitions = new StageTransition[]
            {
                PlayDefaultSong, //When choosed a bird
                () =>
                {
                    foreach(Transform child in notePiano.GetComponentsInChildren<Transform>())
                    {
                        if (child.GetComponent<Transition>() != null){
                            child.GetComponent<Transition>().TransitionOut();
                        }
                    }
                    cButtons.SetActive(false);
                    keyboard.SetActive(true);
                    keyboard.transform.DOMoveY(-2.3f, 2f);
                    cameraAnim.Play("CameraMove");
                    UnfoldBanner();
                },
                () =>
                {
                    cameraAnim.Play("CameraMoveBack");
                    foreach(Transition t in keyboard.gameObject.GetComponentsInChildren<Transition>())
                    {
                        t.TransitionOut();
                    }
                    Invoke("DisableKeyboardAndBanner", 1f);
                    PlaySong();
                }

            };
            rTransitions = new StageTransition[]
            {
                () =>
                {

                },
                () =>
                {
                    foreach(Transform child in notePiano.GetComponentsInChildren<Transform>())
                    {
                        if (child.GetComponent<Transition>() != null){
                            child.GetComponent<Transition>().TransitionOut();
                        }
                    }
                    cameraAnim.Play("CameraZoomBackward");
                    cButtons.SetActive(true);
                    ChangeColorSelection(true);
                },
                () =>
                {
                    cameraAnim.Play("CameraZoom");
                    notePiano.SetActive(false);
                    notePiano.SetActive(true);
                    newSong.TransitionIn();
                    done.TransitionIn();
                    playSong.TransitionIn();
                    hand.SetActive(false);
                    background.GetComponent<Collider2D>().enabled = false;
                }
            };
        }

        private void DisableKeyboardAndBanner()
        {
            GameObject bird = new GameObject();
            foreach (Transform child in birds.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    bird = child.gameObject;
                }
            }

        }
        private void PlayDefaultSong()
        {
            mask.transform.DOMoveX(13.5f, 5f);
            this.GetComponent<AudioSource>().Play();
            cameraAnim.Play("CameraZoom");
            StartCoroutine(NotesFadeInOut());
            ChangeColorSelection(false);
        }

        private void ChangeColorSelection(bool i)
        {
            foreach (Transform child in cButtons.transform)
            {
                child.GetComponent<Collider2D>().enabled = i;
            }
        }

        private IEnumerator NotesFadeInOut()
        {

            foreach (Transform child in defualtNotes.transform)
            {
                Sequence newSequence = DOTween.Sequence();
                newSequence.Append(child.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f));
                newSequence.AppendInterval(1.5f);
                newSequence.Append(child.GetComponent<SpriteRenderer>().DOFade(0f, 2f));
                newSequence.Play();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(2f);
            //Looks redundant I know .. make sure the onEnable function calls
            notePiano.SetActive(false);
            notePiano.SetActive(true);
        }

        private void UnfoldBanner()
        {
            GameObject bird = new GameObject();
            foreach (Transform child in birds.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    bird = child.gameObject;
                }
            }

            foreach(Transform child in bird.transform)
            {
                if (child.gameObject.name.Equals("Banner"))
                {
                    child.gameObject.SetActive(true);
                }
            }
            //Debug.Log("Name of Bird is: " + bird.name);
            bird.GetComponent<Animator>().Play("banner_unfold");
            scrollOpen.Play();
        }

        private void SendBird()
        {
            string bird = "B";
            string text = "";
            foreach(Transform child in birds.transform) {
                if (child.gameObject.activeInHierarchy){
                    bird = child.gameObject.name;
                    text = child.GetComponentInChildren<TextMeshPro>().text;
                }
            }
            int birdnumber = -1;
            switch (bird)
            {
                case "B":
                    birdnumber = 0;
                    break;
                case "G":
                    birdnumber = 1;
                    break;
                case "O":
                    birdnumber = 2;
                    break;
                case "P":
                    birdnumber = 3;
                    break;
                default:
                    break;

            }

            int numbercode = birdnumber * 100 + text.Length;
            network.SendAction(GAME_CODE, numbercode, text + songString);
            network.InternetQuit();
            SceneManager.LoadScene("EndScene");
        }

        public void AddNote(string note)
        {
            if (gameStage == 1 && !playingSong)
            {
                songString = songString + note;
                int index = int.Parse(note);
                song.Add(audioClips[index]);
                birdNote.AddNote(index);
                OnSongChanges();
            }
        }

        [ContextMenu("PlaySong")]
        public void PlaySong()
        {
            StartCoroutine(PlaySongEnum());
            StartCoroutine(ComposedNotesPlayEnum());
        }

        public IEnumerator PlaySongEnum()
        {
            AudioSource[] copySong = new AudioSource[song.Count];
            System.Array.Copy(song.ToArray(), copySong, song.Count);
            foreach (AudioSource note in copySong)
            {
                yield return new WaitForSeconds(PLAYBACK_INTERVAL);
                note.Play();
            }
        }

        //Ending happens here.
        public IEnumerator ComposedNotesPlayEnum()
        {
            if (!playingSong)
            {
                playingSong = true;
                foreach (GameObject note in birdNote.GetNotes())
                {
                    yield return new WaitForSeconds(PLAYBACK_INTERVAL);
                    note.GetComponent<SpriteRenderer>().DOFade(0f, 0.15f).SetLoops(2, LoopType.Yoyo);
                }
                playingSong = false;

                if (gameStage == 3)
                {
                    Invoke("SendBird", SEND_DELAY);
                    foreach (Animator animator in birds.GetComponentsInChildren<Animator>())
                    {
                        animator.Play("full_departure");
                    }

                    staff.GetComponent<SpriteRenderer>().DOFade(0f, 1f);

                    foreach (GameObject g in birdNote.GetNotes())
                    {
                        g.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
                    }
                    playingSong = false;
                }
            }
            
        }
         
        public void ClearSong()
        {
            song = new List<AudioSource>();
            birdNote.ClearSong();
            songString = "";
            OnSongChanges();
        }

        public void OnSongChanges()
        {
            if (song.Count == 0)
            {
                newSong.TransitionOut();
                done.TransitionOut();
                playSong.TransitionOut();
                //closeButton.TransitionIn();
            }
            else if (song.Count == 1)
            {
                newSong.TransitionIn();
                done.TransitionIn();
                playSong.TransitionIn();
                //closeButton.gameObject.SetActive(false);
            } else if (song.Count == 10)
            {
                ProceedTo(2);
            }
        }

        //This method prompts the game to proceed.
        public void Proceed()
        {
            Debug.Log("GameState" + gameStage);
            transitions[gameStage]();
            gameStage++;
        }

        public void ProceedTo(int target)
        {
            Debug.Log("Target is" + target + "Game Stage is" + gameStage);
            if (target < gameStage)
            {
                for (int i = gameStage; i > target; i--)
                {
                    rTransitions[i]();
                    gameStage--;
                }
            } else if (target > gameStage)
            {
                for(int i = gameStage; i < target; i++)
                {
                    transitions[i]();
                    gameStage++;
                }
            }
        }

        public void ToggleRecording()
        {
            if (gameStage == 1)
            {
                ProceedTo(0);
                ClearSong();         
            } else if (gameStage == 2)
            {
                ProceedTo(1);
            }
        }

        public void ChangeBirdColor(string name)
        {
            foreach(Transform child in birds.transform)
            {
                if (child.gameObject.name.Equals(name))
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        void IGameController.StartGame()
        {
            StartCoroutine(buttonsFadeIn());
        }

        private IEnumerator buttonsFadeIn()
        {
            yield return new WaitForSeconds(4.4f);

            foreach (Transform child in cButtons.transform)
            {
                child.GetComponent<SpriteRenderer>().DOFade(1f, 1f);
                child.GetComponent<Collider2D>().enabled = true;
            }
        }

        public int GetGameStage()
        {
            return gameStage;
        }
    }
}
