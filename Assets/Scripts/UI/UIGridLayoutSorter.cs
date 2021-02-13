using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    public abstract class UIGridLayoutSorter<V, M> : MonoBehaviour where V : IMovableUI
    {
        private GridLayoutGroup grid;

        [SerializeField] protected float moveSpeed;
        protected int maxRowCount;
        protected List<V> views = new List<V>();
        protected Dictionary<V, Vector2> startPositions = new Dictionary<V, Vector2>();
        protected int movingViewNum;

        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            grid = GetComponent<GridLayoutGroup>();

            maxRowCount = (int)((rectTransform.rect.width - grid.padding.left - grid.padding.right + grid.spacing.x) /
                (grid.cellSize.x + grid.spacing.x));
        }

        protected void Sort()
        {
            StopAllCoroutines();
            movingViewNum = 0;

            for (int i = 0; i < views.Count; ++i)
            {
                startPositions[views[i]] = views[i].RectTransform.anchoredPosition + views[i].Container.anchoredPosition;
            }

            for (int i = 0; i < views.Count; ++i)
            {
                int targetColumn = i / maxRowCount;
                int targetRow = i % maxRowCount;
                Vector2 startPos = startPositions[views[i]];
                ++movingViewNum;
                StartCoroutine(MoveTo(targetColumn, targetRow, startPos, views[i].Container, moveSpeed));
            }
        }

        private IEnumerator MoveTo(int column, int row, Vector2 start, RectTransform target, float speed)
        {
            Vector2 dest = new Vector2(grid.padding.left + row * (grid.cellSize.x + grid.spacing.x) + grid.cellSize.x / 2, -(grid.padding.top + (column * (grid.cellSize.y + grid.spacing.y) + grid.cellSize.y / 2)));
            Vector2 anchoredDest = dest - start;
            yield return null;
            while ((anchoredDest - target.anchoredPosition).sqrMagnitude > .001f)
            {
                target.anchoredPosition = Vector2.Lerp(target.anchoredPosition, anchoredDest, speed * Time.deltaTime);
                yield return null;
            }
            target.anchoredPosition = anchoredDest;
            --movingViewNum;
            CheckEnd();
        }

        private void CheckEnd()
        {
            if(movingViewNum == 0)
            {
                for(int i = 0; i < views.Count; ++i)
                {
                    views[i].RectTransform.SetAsLastSibling();
                    views[i].Container.anchoredPosition = Vector2.zero;
                }
                OnMoveEnd();
            }
        }

        protected virtual void OnMoveEnd() { }

        public void AddView(V view)
        {
            views.Add(view);
        }

        public void RemoveView(V view)
        {
            views.Remove(view);
        }

    }
}
