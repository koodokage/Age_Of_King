using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    /// <summary>
    /// -NON MEMORY LEAK- INFINITE SCROLL
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class InfiniteScroll : AProducibleUI, IEndDragHandler, IBeginDragHandler,IScrollHandler
    {
        enum Way { NONE, UP, DOWN }

        [SerializeField] RectTransform content;
        [SerializeField] float verticalOffset = 125;
        [SerializeField] float horizontalOffset = 120;
        [Header("Disappear Bounds")]
        [SerializeField] private int botBound = -55;
        [SerializeField] private int topBound = +560;

        ScrollRect _scrollRect;
        Way _currentWay = Way.NONE;
        float _lastScrollValue = 0;
        private const float SCROLL_STOP_MAGNITUDE = 100f;

        public override void Initialize()
        {
            Assert.IsNotNull(content);

            _scrollRect = GetComponent<ScrollRect>();

            Assert.IsNotNull(_scrollRect);

            int count = _scrollRect.content.childCount;
            for (int i = 0; i < count; i++)
            {
                RectTransform childRect = _scrollRect.content.GetChild(i).GetComponent<RectTransform>();
                if (i == 0)
                {
                    childRect.anchoredPosition = new Vector2(horizontalOffset, verticalOffset);
                    continue;
                }

                childRect.anchoredPosition = new Vector2(horizontalOffset, verticalOffset - (verticalOffset * i));
            }
        }

        public void GetScrollChangedValue()
        {
            if (_scrollRect == null)
                return;

            if (_scrollRect.velocity.sqrMagnitude < SCROLL_STOP_MAGNITUDE)
            {
                _currentWay = Way.NONE;
                return;
            }

            DetectMovementWay();
        }

        void DetectMovementWay()
        {
            float diff = _lastScrollValue - content.anchoredPosition.y;
            if (diff > 0)
            {
                _currentWay = Way.DOWN;

            }
            else
            {
                _currentWay = Way.UP;
            }

            _lastScrollValue = content.anchoredPosition.y;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _currentWay = Way.NONE;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastScrollValue = content.anchoredPosition.y;
        }

        private void Update()
        {
            //Nothing to perform
            if (_currentWay == Way.NONE)
                return;


            if (_currentWay == Way.DOWN)
            {
                //Get last index,
                //check bot dissapear bound
                //change first element location with calculated offset
                //set sibling for stay sorted 
                int lastIndex = content.childCount - 1;
                RectTransform lastChildRect = content.GetChild(lastIndex).GetComponent<RectTransform>();
                Vector3 wPos = lastChildRect.TransformPoint(_scrollRect.GetComponent<RectTransform>().anchoredPosition);

                if ((wPos.y) < botBound)
                {
                    RectTransform firstChildRect = content.GetChild(0).GetComponent<RectTransform>();
                    Vector2 anchoredF = firstChildRect.anchoredPosition;
                    anchoredF.y += verticalOffset;
                    lastChildRect.anchoredPosition = anchoredF;
                    lastChildRect.SetAsFirstSibling();
                }
            }

            if (_currentWay == Way.UP)
            {
                //Get first index,
                //check top dissapear bound
                //change last element location with calculated offset
                //set sibling for stay sorted 
                int lastIndex = content.childCount - 1;
                RectTransform firstChildRect = content.GetChild(0).GetComponent<RectTransform>();
                Vector3 wPos = firstChildRect.TransformPoint(_scrollRect.GetComponent<RectTransform>().anchoredPosition);

                if ((wPos.y) > topBound)
                {
                    RectTransform lastChildRect = content.GetChild(lastIndex).GetComponent<RectTransform>();
                    Vector2 anchoredF = lastChildRect.anchoredPosition;
                    anchoredF.y -= verticalOffset;
                    firstChildRect.anchoredPosition = anchoredF;
                    firstChildRect.SetAsLastSibling();
                }
            }

        }

        public void OnScroll(PointerEventData eventData)
        {
            DetectMovementWay();
        }

    }

}