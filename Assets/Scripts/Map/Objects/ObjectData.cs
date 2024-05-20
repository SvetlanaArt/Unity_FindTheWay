using System;
using System.Collections.Generic;
using UnityEngine;

namespace FindTheWay.Map.Objects
{
    /// <summary>
    /// Contain data about types of map object 
    /// </summary>
    [CreateAssetMenu(fileName = "NewObjectData", menuName = "ScriptableObjects/ObjectData", order = 1)]
    public class ObjectData : ScriptableObject
    {
        [SerializeField]
        List<DataSet> dataSet = new List<DataSet>();

        [Serializable]
        struct DataSet
        {
            public ElementType type;
            public GameObject prefab;
        }

        public GameObject GetPrefab(ElementType type)
        {
            foreach (DataSet data in dataSet)
            {
                if (data.type == type)
                {
                    return data.prefab;
                }
            }
            return null;
        }

    }

}
