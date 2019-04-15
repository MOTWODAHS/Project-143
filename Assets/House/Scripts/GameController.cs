using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;
using System.IO;

namespace Loving
{
    class GameController : MonoBehaviour, IGameController
    {

        int gameStage = 0;

        private bool gameIsOver = false;

        private string sendStr = "";

        private const float DELAY_TO_SEND = 4.5f;

        private Vector3 nameTagPos;

        private Vector3 nameTagScale;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        internal Color selectedColor;

        internal bool colorSelected = false;

        [Header("Cover")]
        public Collider2D cover;

        [Header("Stage1")]
        public GameObject mask1;

        [Header("Stage2")]
        public GameObject mask2;
        public GameObject palette;
        public GameObject nameTag;

        [Header("Stage3")]
        public GameObject enterName;
        public GameObject blueprint;
        public GameObject pencilButtonObj;
        public TextInputField addNameTag;
        //public GameObject nameTag;

        [Header("Stage4")]
        public GameObject altBlueprint;
        public Transform pivot;
        public Animator altBlueprintAnim;
        public Animator envelopeAnim;
        public RenderTexture texture;
        public NetworkingController network;
        public GameObject endUI;


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
                        nameTag.GetComponent<Collider2D>().enabled = true;
                        
                          
                       
                    }
                },
                () =>
                {
                    enterName.transform.DOMoveY(-4f, 2f);
                    nameTagPos = nameTag.transform.position;
                    nameTagScale = nameTag.transform.localScale;
                    nameTag.transform.DOMove(new Vector3(-0.2807f, -1.7288f, 0f), 2f);
                    nameTag.transform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 2f);
                    foreach(Collider2D collider in blueprint.GetComponentsInChildren<Collider2D>())
                    {
                        collider.enabled = false;
                    }
                    pencilButtonObj.SetActive(false);
                    addNameTag.enabled = true;
                },
                () =>
                {
                    nameTag.transform.localScale = nameTagScale;
                    enterName.transform.DOMoveY(-13f,1f);
                    enterName.SetActive(false);
                    nameTag.transform.position = nameTagPos;
                    StartCoroutine(SaveHouseTexture());
                    Camera.main.DOOrthoSize(11f, 5f);
                    Camera.main.transform.DOScale(1.57f, 5f);
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("Default"));
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("InImageRender"));
                    altBlueprint.SetActive(true);
                    pivot.DORotate(new Vector3(0, 180, 0), 2f).OnComplete(() =>
                    {
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

        private void SendInfoToNetwork()
        {
            network.SendAction(4, -1, sendStr);
            endUI.SetActive(true);
            network.InternetQuit();
        }

        void IGameController.StartGame()
        {
            cover.gameObject.SetActive(false);
        }

        void IGameController.Proceed()
        {
            transitions[gameStage]();
            gameStage++;
        }

        public int GetGameStage()
        {
            return gameStage;
        }
    }
}
