using System.Collections.Generic;
using UnityEngine;

namespace Sonn.SproutJourney
{
    public class GridManager : MonoBehaviour
    {
        public GameObject mapPrefabs;
        public List<Vector3Int> mapCoordinates;
        public Vector3 position;

        private float m_mapSize = 1f;
        private Dictionary<Vector3Int, GameObject> m_mapInstances;

        private void Awake()
        {
            m_mapInstances = new Dictionary<Vector3Int, GameObject>();
        }

        private void Start()
        {
            CreateMap();
            CreatePos();
        }

        private void CreateMap()
        {
            ClearMap();

            if (!mapPrefabs)
            {
                return;
            }    
            foreach (var coord in mapCoordinates)
            {
                Vector3 worldPos = new(coord.x * m_mapSize, coord.y * m_mapSize, coord.z * m_mapSize);
                var map = Instantiate(mapPrefabs, worldPos, Quaternion.identity, transform);
                m_mapInstances[coord] = map;
            }    
        }
        
        private void ClearMap()
        {
            if (transform)
            {
                for (int i = transform.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(transform.GetChild(i).gameObject);
                }    
            }
            m_mapInstances.Clear();
        }
        
        private void CreatePos()
        {
            transform.position = new(transform.position.x + position.x, 
                                     transform.position.y + position.y,  
                                     transform.position.z + position.z);
        }

        public Vector3Int WorldToGrid(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - transform.position;
            return new Vector3Int(
                Mathf.RoundToInt(localPos.x / m_mapSize),
                Mathf.RoundToInt(localPos.y / m_mapSize),
                Mathf.RoundToInt(localPos.z / m_mapSize)
            );
        }

        public bool IsInsideGrid(Vector3 worldPos)
        {
            Vector3Int gridPos = WorldToGrid(worldPos);
            return mapCoordinates.Contains(gridPos);
        }
    }
}
