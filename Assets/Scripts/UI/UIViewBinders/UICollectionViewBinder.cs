using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public abstract class UICollectionViewBinder<T, M, E> : UIViewBinder<Transform> 
        where T : MonoBehaviour, IViewTemplate<M> 
        where M : class
        where E : UnityEvent<T>
    {
        [SerializeField] private T template;
        private List<T> spawnedViews = new List<T>();

        [SerializeField] private E addEvent, removeEvent;

        protected override void Bind(UIViewModel viewModel)
        {
            var ad = Delegate.CreateDelegate(typeof(Action<M>), this, "OnAddEvent");
            var rd = Delegate.CreateDelegate(typeof(Action<M>), this, "OnRemoveEvent");
            var bindedList = viewModel.BindCollectionProperty(bindPropertyName, ad, rd) as EventList<M>;
            for(int i = 0; i < bindedList.Count; ++i)
            {
                var item = Spawn(bindedList[i]);
                item.gameObject.SetActive(true);
                spawnedViews.Add(item);
                addEvent?.Invoke(item);
            }
        }

        private T Spawn(M model)
        {
            var tmp = Instantiate(template, bindView);
            tmp.transform.localScale = Vector3.one;
            tmp.transform.localPosition = Vector3.zero;
            tmp.transform.localRotation = Quaternion.identity;
            tmp.Init(model);
            return tmp;
        }

        private void OnAddEvent(M model)
        {
            var item = Spawn(model);
            spawnedViews.Add(item);
            addEvent?.Invoke(item);
        }

        private void OnRemoveEvent(M model)
        {
            for(int i = 0; i < spawnedViews.Count; ++i)
            {
                if(spawnedViews[i].Model == model)
                {
                    //TODO: Object Pooling 해야
                    var item = spawnedViews[i];
                    Destroy(item.gameObject);
                    spawnedViews.RemoveAt(i);
                    removeEvent?.Invoke(item);
                    break;
                }
            }
        }

    }
}
