using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

namespace Loving
{
    class GameController : MonoBehaviour, IGameController
    {

        int gameStage = 0;

        private bool gameIsOver = false;

        private string sendStr = "";

        private const float DELAY_TO_SEND = 7.5f;

        private const float TIMER_LIMIT = 200f;

        private float timer = 0f;

        private Vector3 nameTagPos;

        private Vector3 nameTagScale;

        private List<DoorWindowPiece> pieces = new List<DoorWindowPiece>();

        private Quaternion nameTagRotation;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        internal Color selectedColor;

        internal bool colorSelected = false;

        [Header("Cover")]
        public Collider2D cover;
        public GameObject UI;

        [Header("Stage1")]
        public GameObject mask1;
        public GameObject doorComponent;
        public GameObject windowComponent;

        [Header("Stage2")]
        public GameObject mask2;
        public GameObject palette;
        public GameObject doneButton;

        [Header("Stage3")]
        public GameObject nameTagComposite;
        public GameObject nameTag;
        public GameObject background;
        public GameObject mask3;

        [Header("Stage4")]
        public GameObject enterName;
        public GameObject blueprint;
        public GameObject pencilButtonObj;
        public TextInputField addNameTag;
        //public GameObject nameTag;
        public Collider doNotTouch;

        [Header("Stage5")]
        public GameObject altBlueprint;
        public Transform pivot;
        public Animator altBlueprintAnim;
        public Animator envelopeAnim;
        public RenderTexture texture;
        public NetworkingController network;
        public GameObject endUI;
        public Censor censor;
        public TextMeshPro textMeshPro;

        [Header("Sounds")]
        public AudioSource pickedUpSound;
        public AudioSource dropDownSound;
        public AudioSource sendObjectToScreen;

        void Start()
        {
            gameStage = 0;
            transitions = new StageTransition[]
            {
                () =>
                {

                },
                () =>
                {
                    if (mask1 != null){
                        mask1.SetActive(false);
                        foreach(Collider2D c in doorComponent.GetComponentsInChildren<Collider2D>())
                        {
                            c.enabled = true;
                        }
                        foreach(Collider2D c in windowComponent.GetComponentsInChildren<Collider2D>())
                        {
                            c.enabled = true;
                        }
                    }
                },
                () =>
                {
                    if (mask2 != null)
                    {
                        mask2.SetActive(false);
                        foreach(Collider2D c in palette.GetComponentsInChildren<Collider2D>())
                        {
                            c.enabled = true;
                        }
                        doneButton.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                        doneButton.GetComponent<Collider2D>().enabled = true;
                    }
                },
                () =>
                {
                    nameTagComposite.SetActive(true);
                    PlayDropDownSound();
                    mask1.SetActive(true);
                    mask2.SetActive(true);
                    mask3.SetActive(true);
                    background.SetActive(false);
                    doneButton.SetActive(false);
                },
                () =>
                {
                    doneButton.SetActive(false);
                    enterName.transform.DOMoveY(-4f, 2f);
                    nameTagPos = nameTag.transform.position;
                    nameTagScale = nameTag.transform.localScale;
                    nameTagRotation = nameTag.transform.rotation;
                    nameTag.transform.DOMove(new Vector3(-0.2807f, -1.7288f, 0f), 2f).OnComplete(()=>{
                        doNotTouch.enabled = true;
                    });
                    nameTag.transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 2f);
                    nameTag.transform.DORotate(new Vector3(0, 0, 0), 2f);
                    foreach(Collider2D collider in blueprint.GetComponentsInChildren<Collider2D>())
                    {
                        collider.enabled = false;
                    }
                    addNameTag.enabled = true;
                },
                () =>
                {
                    UI.SetActive(false);
                    //ResetNameTag
                    nameTag.transform.localScale = nameTagScale;
                    enterName.transform.DOMoveY(-13f,1f);
                    enterName.SetActive(false);
                    nameTag.transform.position = nameTagPos;
                    nameTag.transform.rotation = nameTagRotation;

                    string text = censor.CensorText( textMeshPro.text);
                    print("text is " + text);
                    text = text.Replace("*", "");
                    print("text is " + text);
                    textMeshPro.GetComponent<TextInputField>().enabled =false;
                    textMeshPro.text = text;
                    
                    
                    //Lighten Up Window
                    foreach(DoorWindowPiece o in pieces)
                    {
                        print("Counting");
                        GameObject firstChild = o.GetComponentsInChildren<Transform>()[1].gameObject;
                        if (firstChild.name.Equals("grey"))
                        {
                            firstChild.GetComponent<SpriteRenderer>().color = new Color(0.97f, 0.84f, 0.05f);
                        }
                    }

                    StartCoroutine(SaveHouseTexture());
                    Camera.main.DOOrthoSize(11f, 5f);
                    Camera.main.transform.DOScale(1.57f, 5f);
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("Default"));
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("InImageRender"));
                    altBlueprint.SetActive(true);
                    altBlueprint.GetComponent<AudioSource>().Play();
                    pivot.DORotate(new Vector3(0, 180, 0), 2f).OnComplete(() =>
                    {
                        StartCoroutine(PlayEndingSound());
                        altBlueprintAnim.Play("ending");
                        envelopeAnim.Play("ending");
                        Invoke("SendInfoToNetwork", DELAY_TO_SEND);
                    });
                   
                },
                () =>
                {

                }
            };
        }

        public void AddPiece(DoorWindowPiece piece)
        {
            pieces.Add(piece);
        }

        public void RemovePiece(DoorWindowPiece piece)
        {
            pieces.Remove(piece);
        }

        [ContextMenu("Send House")]
        private IEnumerator SaveHouseTexture()
        {
            yield return new WaitForEndOfFrame();
            Texture2D houseImage = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
            RenderTexture.active = texture;
            houseImage.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            houseImage.Apply();
            byte[] bytes;
            bytes = houseImage.EncodeToPNG();

            var dirPath = Application.dataPath + "/SaveImages/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            Debug.Log(dirPath);
            File.WriteAllBytes(dirPath + "Image" + ".png", bytes);

            sendStr = Convert.ToBase64String(bytes);
            RenderTexture.active = null;
        }

        private IEnumerator PlayEndingSound()
        {
            sendObjectToScreen.Play();
            AudioSource[] sounds = envelopeAnim.GetComponents<AudioSource>();
            sounds[0].Play();
            yield return new WaitForSeconds(1.5f);

            sounds[0].Stop();
            sounds[1].Play();
            yield return new WaitForSeconds(1f);

            sounds[1].Stop();
            sounds[2].Play();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > TIMER_LIMIT)
            {
                SceneManager.LoadScene("HoldScene");
            } 
        }

        private void SendInfoToNetwork()
        {
            network.SendAction(4, -1, sendStr);
            network.InternetQuit();
            SceneManager.LoadScene("EndScene");
        }

        public void StartGame()
        {
            cover.gameObject.SetActive(false);
        }

        public void Proceed()
        {
            transitions[gameStage]();
            gameStage++;
            ResetTimer();
        }

        public int GetGameStage()
        {
            return gameStage;
        }

        public void PlayPickUpSound()
        {
            pickedUpSound.Play();
        }

        public void PlayDropDownSound()
        {
            dropDownSound.Play();
        }

        public void ResetTimer()
        {
            timer = 0f;
        }
    }
}
