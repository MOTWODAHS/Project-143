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

        

        private int gameStage = 0;
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
                    mask1.SetActive(false);
                },
                () =>
                {
                    mask2.SetActive(false);
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
                    Camera.main.DOOrthoSize(11f, 5f);
                    Camera.main.cullingMask &=  ~(1 << LayerMask.NameToLayer("Default"));
                    altBlueprint.SetActive(true);
                    pivot.DORotate(new Vector3(0, 180, 0), 5f).OnComplete(() =>
                    {
                        altBlueprintAnim.Play("ending");
                        envelopeAnim.Play("ending");
                    });
                   
                }
            };
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

        
    }
}
