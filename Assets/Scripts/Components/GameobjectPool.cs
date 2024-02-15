using UnityEngine;
using UnityEngine.Pool;

namespace AgeOfKing.Components
{
    public abstract class GameobjectPool<T>:MonoBehaviour where T: MonoBehaviour 
    {
        ObjectPool<T> _pool;
        T _prefab;
        protected Transform _root;

        public void Initialize(T prefab,Transform root,int poolSize = 10, int maxPoolSize = 50)
        {
            _prefab = prefab;
            _root = root;
            _pool = new ObjectPool<T>(() =>
            {
                return OnCreate();

            }, poolElement =>
            {
                poolElement.gameObject.SetActive(true);

            }, poolElement =>
            {
                poolElement.gameObject.SetActive(false);

            }, poolElement =>
            {
                //Destroy(projectile);
            }, false, poolSize, maxPoolSize);
        }

        public virtual T OnCreate()
        {
            return GameObject.Instantiate(_prefab, _root);
        }


        public T Get
        {
            get => _pool.Get();
        }

        public void Put(T target)
        {
            _pool.Release(target);
            target.transform.SetParent(null);
        }

        public void Put(T target,Transform targetParent)
        {
            _pool.Release(target);
            target.transform.SetParent(targetParent);
        }


        public static GameObject InstantiatePrefab(GameObject prefab)
        {
            return GameObject.Instantiate(prefab);
        }
    }

}