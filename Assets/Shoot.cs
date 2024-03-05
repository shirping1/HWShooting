using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public Transform Arrow_transform;
    public float shoot_speed;
    // 표적이 생성될 위치와 프리팹, 속도
    public Transform SpawnPoint;
    public GameObject TargetPrefab;
    public float move_speed;
    public float spawn_time;
    float time = 0.0f;

    void LookAt()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mouse_position.x-Arrow_transform.position.x,mouse_position.y-Arrow_transform.position.y);
        Arrow_transform.right = dir;
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(ArrowPrefab, Arrow_transform.position, Arrow_transform.rotation);
            go.GetComponent<Rigidbody2D>().velocity = Arrow_transform.transform.right * shoot_speed;
            Destroy(go, 1.0f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void SpawnTarget()
    {
        GameObject target = Instantiate(TargetPrefab, SpawnPoint.position, Quaternion.identity);
        float scale = Random.Range(0.1f, 0.5f);
        target.transform.localScale = new Vector2(scale, scale);
        target.GetComponent<Rigidbody2D>().velocity = new Vector2(-move_speed, 0.0f);
        Destroy(target, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        Fire();
        if (spawn_time <= time)
        {
            SpawnTarget();
            time = 0;
            spawn_time = Random.Range(0.5f, 2.0f);
        }
        time += Time.deltaTime;
    }
}
