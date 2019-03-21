using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Singing
{
    public class GameController : MonoBehaviour
    {

        private int gameStage;

        private string songString;

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

        [Header("Camera")]
        public GameObject cam;

        [Header("NotePiano")]
        public GameObject notePiano;

        [Header("Birds")]
        public GameObject birds;
        public Collider2D staff;

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


        private void Start()
        {
            gameStage = 0;
            cameraAnim = cam.GetComponent<Animator>();
            transitions = new StageTransition[]
            {
                PlayDefaultSong, //WHen choosed a bird
                () =>
                {
                    newSong.TransitionOut();
                    done.TransitionOut();
                    PlaySong();
                }, //When hits done;
                () =>
                {
                    foreach(Transform child in notePiano.GetComponentsInChildren<Transform>())
                    {
                        if (child.GetComponent<Transition>() != null){
                            child.GetComponent<Transition>().TransitionOut();
                        }
                    }
                    cameraAnim.Play("CameraZoomBackward");
                    cButtons.SetActive(false);
                    staff.enabled = true;
                    hand.SetActive(true);
                    background.GetComponent<Collider2D>().enabled = true;
                },
                () =>
                {
                    PlaySong();
                    hand.SetActive(false);
                    staff.enabled = false;
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
                    
                },
                () =>
                {
                    cameraAnim.Play("CameraZoom");
                    notePiano.SetActive(false);
                    notePiano.SetActive(true);
                    newSong.TransitionIn();
                    done.TransitionIn();
                    hand.SetActive(false);
                    background.GetComponent<Collider2D>().enabled = false;
                }
            };
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
                newSequence.Append(child.GetComponent<SpriteRenderer>().DOFade(0f, 1f));
                newSequence.Play();
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
            //Looks redundant I know .. make sure the onEnable function calls
            notePiano.SetActive(false);
            notePiano.SetActive(true);
        }

        public void AddNote(string note)
        {
            if (gameStage == 1)
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
            foreach (AudioSource note in song)
            {
                yield return new WaitForSeconds(0.3f);
                note.Play();
            }
        }

        public IEnumerator ComposedNotesPlayEnum()
        {
            Debug.Log(gameStage);
            foreach (GameObject note in birdNote.GetNotes())
            {
                yield return new WaitForSeconds(0.3f);
                note.GetComponent<SpriteRenderer>().DOFade(0f, 0.15f).SetLoops(2, LoopType.Yoyo);
            }
            if (gameStage == 4)
            {
                birds.transform.DOMove(new Vector3(11, 11, 0), 20f);
                staff.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
                foreach(GameObject g in birdNote.GetNotes())
                {
                    g.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
                }
            }
        }

        public void ClearSong()
        {
            song = new List<AudioSource>();
            birdNote.ClearSong();
            OnSongChanges();
        }

        public void OnSongChanges()
        {
            Debug.Log(song.Count);
            if (song.Count == 0)
            {
                newSong.TransitionOut();
                done.TransitionOut();
            }
            else if (song.Count == 1)
            {
                newSong.TransitionIn();
                done.TransitionIn();
            } else if (song.Count == 10)
            {
                ProceedTo(3);
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
            if (gameStage == 2)
            {
                Proceed();
           
            } else if (gameStage == 3)
            {
                ProceedTo(1);
            } else {
                ProceedTo(0);
                ClearSong();
            }
        }

        public void ChangeBirdColor(string name)
        {
            foreach(Transform child in birds.transform)
            {
                if (child.gameObject.name.Equals(name))
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        public void StartGame()
        {
            foreach(Transform child in cButtons.transform)
            {
                child.GetComponent<SpriteRenderer>().DOFade(1f, 1f);
                child.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
