using UnityEngine;
using AgeOfKing.Systems.Input;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Components
{
    public class GridInput : MonoBehaviour
    {
        Camera _mainCamera;

        [SerializeField] Tilemap map;
        [SerializeField] ITilemap imap;
        [SerializeField] LayerMask playerPawnInputLayer;
        [SerializeField] GameObject symbol;
        [SerializeField] GameObject selectedPawn;
        [SerializeField] Vector3 targetLocation;
        [SerializeField] bool movementToggle = false;
        [SerializeField] bool isMoving = false;


        private void Awake()
        {
            _mainCamera = Camera.main;

            Assert.IsNotNull(_mainCamera);

        }


        private void OnEnable()
        {
            //Listen inputs
            InputManager.OnMousePositionChanged += OnChange_MousePosition;
            InputManager.OnMouseLeftClicked += OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked += OnClicked_MouseRight;
        }

        private void OnDisable()
        {
            //Release inputs
            InputManager.OnMousePositionChanged -= OnChange_MousePosition;
            InputManager.OnMouseLeftClicked -= OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked -= OnClicked_MouseRight;
        }

        /// <summary>
        /// Move unit input trigger
        /// </summary>
        private void OnClicked_MouseRight()
        {

            if (selectedPawn == null)
                return;

            if (isMoving)
                return;

            movementToggle = true;
            targetLocation = symbol.transform.position;
        }

        /// <summary>
        /// Select unit input trigger
        /// </summary>
        void OnClicked_MouseLeft(Vector2 mouseLocation)
        {
            if (isMoving)
                return;

            Ray ray = _mainCamera.ScreenPointToRay(mouseLocation);
            RaycastHit2D rayHit;

            if (rayHit = Physics2D.Raycast(ray.origin, ray.direction, 100, playerPawnInputLayer))
            {
                selectedPawn = rayHit.transform.gameObject;
            }
        }

        /// <summary>
        /// Transform pointer data to ground map cell location
        /// </summary>
        /// <param name="mouseLocation"></param>
        void OnChange_MousePosition(Vector2 mouseLocation)
        {
            Vector3 mouseLoc = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mouseCell = map.WorldToCell(mouseLoc);
            var tile = map.GetTile(mouseCell);
            if (tile != null)
            {
                symbol.transform.position = mouseCell;
            }
        }

        private void Update()
        {
            //TEMPO CODE 

            if (selectedPawn == null)
                return;

            if (movementToggle == false)
                return;

            if ((targetLocation - selectedPawn.transform.position).magnitude < 0.1f)
            {
                selectedPawn.transform.position = targetLocation;
                movementToggle = false;
                selectedPawn = null;
                isMoving = false;
                return;
            }

            selectedPawn.transform.position = Vector3.MoveTowards(selectedPawn.transform.position, targetLocation, Time.deltaTime * 4f);
            isMoving = true;
        }


    }

}
