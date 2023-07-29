using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Room_Data : SerializedMonoBehaviour
{
    public string Room_name;
    public Dictionary<string, PolygonCollider2D> Spawn_pos = new Dictionary<string, PolygonCollider2D>();
    public Dictionary<string, List<Item_Data>> Items = new Dictionary<string, List<Item_Data>>();
    public Dictionary<string, Toggle_Object> Toggles = new Dictionary<string, Toggle_Object>();

    List<string> keys = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (string Key in Spawn_pos.Keys) { keys.Add(Key); }
    }

    private void Start()
    {
        Spawn_Item();
        Spawn_Item();
        Spawn_Item();
        Spawn_Item();
        Spawn_Item();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button("Spawn_Item")]
    public void Spawn_Item()
    {
        foreach (string Key in Spawn_pos.Keys) { Spawn_pos[Key].enabled = true; }
        string set_key = null;
        try { set_key = keys[Random.Range(0, keys.Count)]; } catch { return; }

        // 스폰 요소 지정
        PolygonCollider2D set_bound = Spawn_pos[set_key];
        Vector2 set_pos = Vector2.zero;
        Item_Data set_item = null;
        try { set_item = Items[set_key][Random.Range(0, Items[set_key].Count)]; }
        catch { return; }
        // 선택된 아이템이 fix 인지 아닌지 따라 고정이거나 범위 내 랜덤 좌표
        if (set_item.IsFix) { set_pos = set_bound.transform.position; } 
        else
        {
            Vector2 b_max_point = Vector2.zero, b_min_point = Vector2.zero;

            // 스폰 범위 최대 최소 지정
            Vector2[] vPoints = set_bound.GetPath(0);
            for (int i = 0; i < vPoints.Length; ++i)
            {
                if (b_max_point == Vector2.zero) { b_max_point = vPoints[i]; }
                if (b_min_point == Vector2.zero) { b_min_point = vPoints[i]; }

                if (vPoints[i].x > b_max_point.x) { b_max_point.x = vPoints[i].x; }
                if (vPoints[i].y > b_max_point.y) { b_max_point.y = vPoints[i].y; }

                if (vPoints[i].x < b_min_point.x) { b_min_point.x = vPoints[i].x; }
                if (vPoints[i].y < b_min_point.y) { b_min_point.y = vPoints[i].y; }
            }
            int chk = 0;
            while (chk < 300)
            {
                chk++;
                if (b_min_point == b_max_point) { set_pos = b_max_point; break; }
                else
                {
                    set_pos.x = Random.Range(b_min_point.x, b_max_point.x);
                    set_pos.y = Random.Range(b_min_point.y, b_max_point.y);
                }
                if (Spawn_pos[set_key].OverlapPoint(transform.TransformPoint(set_pos))) { break; }
            }
        }

        switch (set_item.Type)
        {
            case Item_Type.Click:
                Clickable_Object c_obj = Object_Pool.SpawnFromPool<Clickable_Object>("Clickable_Object", Vector3.zero);
                c_obj.transform.SetParent(this.transform);
                c_obj.Set_Obj(set_item);
                c_obj.transform.position = transform.TransformPoint(set_pos) + Vector3.back;
                break;
            case Item_Type.Drag:
                Dragable_Object d_obj = Object_Pool.SpawnFromPool<Dragable_Object>("Dragable_Object", Vector3.zero);
                d_obj.transform.SetParent(this.transform);
                d_obj.Set_Obj(set_item);
                d_obj.transform.position = transform.TransformPoint(set_pos) + Vector3.back;
                break;
            case Item_Type.Scrub:
                Scrubable_Object s_obj = Object_Pool.SpawnFromPool<Scrubable_Object>("Scrubable_Object", Vector3.zero);
                s_obj.transform.SetParent(this.transform);
                s_obj.Set_Obj(set_item);
                s_obj.transform.position = transform.TransformPoint(set_pos) + Vector3.back;
                break;
        }
        foreach (string Key in Spawn_pos.Keys) { Spawn_pos[Key].enabled = false; }

    }
}
