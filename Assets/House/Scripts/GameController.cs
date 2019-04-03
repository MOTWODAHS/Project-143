using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Loving
{
    class GameController : MonoBehaviour, IGameController
    {
        [Header("Cover")]
        public GameObject cover;

        int gameStage = 0;
        private bool gameIsOver = false;

        private delegate void StageTransition();

        private StageTransition[] transitions;

        [Header("Stage1")]
        public GameObject mask1;

        [Header("Stage2")]
        public GameObject mask2;

        [Header("Stage3")]
        public GameObject enterName;
        public GameObject blueprint;
        public GameObject pencilButtonObj;
        public AddNameTag addNameTag;

        [Header("Stage4")]
        public GameObject altBlueprint;
        public Transform pivot;
        public Animator altBlueprintAnim;
        public Animator envelopeAnim;
        public RenderTexture texture;


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
                    }
                },
                () =>
                {
                    enterName.GetComponent<RectTransform>().DOMoveY(110f, 2f);
                    foreach(Collider2D collider in blueprint.GetComponentsInChildren<Collider2D>())
                    {
                        collider.enabled = false;
                    }
                    pencilButtonObj.SetActive(false);
                    addNameTag.enabled = true;
                },
                () =>
                {
                    SendInfoToNetwork();
                    Camera.main.DOOrthoSize(11f, 5f);
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("Default"));
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("InImageRender"));
                    altBlueprint.SetActive(true);
                    pivot.DORotate(new Vector3(0, 180, 0), 5f).OnComplete(() =>
                    {
                        altBlueprintAnim.Play("ending");
                        envelopeAnim.Play("ending");
                    });
                   
                }
            };
        }

        [ContextMenu("Send House")]
        private void SendInfoToNetwork()
        {
            Texture2D houseImage = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
            RenderTexture.active = texture;
            houseImage.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            houseImage.Apply();
            byte[] bytes;
            bytes = houseImage.EncodeToPNG();

            string path = Application.persistentDataPath + "/p.png";
            Debug.Log("Image saved to: " + path);
            System.IO.File.WriteAllBytes(path, bytes);
            RenderTexture.active = null;
        }

        void IGameController.StartGame()
        {
            cover.SetActive(false);
        }

        void IGameController.Proceed()
        {
            Debug.Log("Game Stage is " + gameStage);
            transitions[gameStage]();
            gameStage++;
        }

        public int GetGameStage()
        {
            return gameStage;
        }
    }
}
