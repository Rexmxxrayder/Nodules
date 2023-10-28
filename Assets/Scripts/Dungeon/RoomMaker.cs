using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class RoomMaker : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject child;
    [SerializeField] private Grid grid;
    [SerializeField] private List<string> list = new();
    [SerializeField] private List<GameObject> notUsed = new();
    [SerializeField] private List<GameObject> used = new();
    [SerializeField] private List<Material> materials = new();

    private void OnValidate() {
        if (grid == null) {
            grid = GetComponent<Grid>();
        }

        MakeRoom();
    }

    private void MakeRoom() {
        notUsed.AddRange(used);
        used.Clear();
        for (int i = 0; i < notUsed.Count; i++) {
            notUsed[i].SetActive(false);
        }

        for (int i = 0; i < list.Count; i++) {
            for (int j = 0; j < list[i].Length; j++) {
                int a = int.Parse(list[i][j].ToString());
                if (a != 0) {
                    Vector3 worldPosition = grid.GetCellCenterWorld(new Vector3Int(j, 0, i));
                    GameObject b;
                    if (notUsed.Count == 0) {
                        b = Instantiate(ground, worldPosition, Quaternion.identity, child.transform);
                        used.Add(b);
                    } else {
                        b = notUsed[0];
                        b.transform.position = worldPosition;
                        used.Add(b);
                        notUsed.RemoveAt(0);
                    }
                    b.GetComponent<MeshRenderer>().material = materials[a - 1];
                }
            }
        }

        for (int i = 0; i < notUsed.Count; i++) {
            notUsed[i].SetActive(false);
        }

        for (int i = 0; i < used.Count; i++) {
            used[i].SetActive(true);
        }
    }
}
