using System;
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
    public class InfiniteScroll : MonoBehaviour, IEndDragHandler, IBeginDragHandler,IScrollHandler
    {
        enum Way { NONE, UP, DOWN }
        ScrollRect _scrollRect;
        RectTransform _owner;
        Way _currentWay = Way.NONE;
        float _lastScrollValue = 0;
        bool _forceStop = false;
        private const float SCROLL_STOP_MAGNITUDE = 100f;

        [Header("Components")]
        [SerializeField] RectTransform content;
        [SerializeField] RectTransform midPoint;
        [Header("Disappear Bounds")]
        [SerializeField] RectTransform topBound;
        [SerializeField] RectTransform botBound;
        [SerializeField] private float neededDiviser = 100;
        [SerializeField] float verticalOffset = 25;
        [SerializeField] float horizontalOffset = 120;

        int _dynamicNeededAmount;

        private void Awake()
        {
            Assert.IsNotNull(content);
            _scrollRect = GetComponent<ScrollRect>();
            Assert.IsNotNull(_scrollRect);
            _owner = GetComponent<RectTransform>();
            Assert.IsNotNull(_owner);

            _forceStop = true;
        }

        public void Launch()
        {
            float midPoint = (_dynamicNeededAmount * neededDiviser)/2;
            var local = content.localPosition;
            local.y = midPoint;
            content.localPosition = local;
            OrderElements();
            _forceStop = false;
        }

        public int GetRequestedAmount(int coreDataCount)
        {
            _forceStop = true;
            content.localPosition = Vector2.zero;
            Vector3 botPos = midPoint.TransformPoint(botBound.localPosition);
            Vector3 topPos = midPoint.TransformPoint(topBound.localPosition);
            int h = Math.Abs((int)topPos.y) - Math.Abs((int)botPos.y);
            _dynamicNeededAmount = h / (int)neededDiviser;
            int ceil = _dynamicNeededAmount % coreDataCount;
            _dynamicNeededAmount -= ceil;
            return _dynamicNeededAmount;
        }

        private void OrderElements()
        {
            int count = _scrollRect.content.childCount;
            float realVerticalOffset = verticalOffset;

            for (int i = 0; i < count; i++)
            {
                RectTransform childRect = _scrollRect.content.GetChild(i).GetComponent<RectTransform>();
                if (i == 0)
                {
                    childRect.anchoredPosition = new Vector2(horizontalOffset, realVerticalOffset);
                    continue;
                }

                childRect.anchoredPosition = new Vector2(horizontalOffset, realVerticalOffset - (realVerticalOffset * i));
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
            float way = _lastScrollValue - content.anchoredPosition.y;
            if (way > 0)
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
            if (_forceStop)
            {
                _scrollRect.velocity = Vector2.zero;
                _currentWay = Way.NONE;
                return;
            }

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
                Vector3 wPos = content.TransformPoint(lastChildRect.localPosition);
                Vector3 botPos = midPoint.TransformPoint(botBound.localPosition);

                if ((wPos.y) < botPos.y)
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
                Vector3 wPos = content.TransformPoint(firstChildRect.localPosition);
                Vector3 topPos = midPoint.TransformPoint(topBound.localPosition);

                if ((wPos.y) > topPos.y)
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