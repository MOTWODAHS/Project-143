using UnityEngine;

namespace Loving
{

    class TapColorSelection : TapableObject
    {
        private GameController game;

        private void Start()
        {
            game = GameObject.FindGameObjectWithTag("gameController").GetComponent <GameController> ();
        }
        public override void OnTap()
        {
            game.colorSelected = true;
            game.selectedColor = GetComponent<ColorPalettePiece>().color;
        }
    }
}
