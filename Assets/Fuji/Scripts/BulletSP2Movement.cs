using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSP2Movement : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float killtime;
    [SerializeField] private float damage;
    [SerializeField] private float angle;
    [SerializeField] private float sp2SteerSpeed;
    public string[] tagsToSearch;
    private GameObject closestEnemy;
    // Start is called before the first frame update

    
    void Start()
    {
        Destroy(this.gameObject,killtime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Search();
    }


    private void Search()
    {
        // 最も近い敵の初期値と最小距離の初期値
        closestEnemy = null;
        float minDistance = Mathf.Infinity;

        // 自分の現在位置
        Vector3 currentPosition = transform.position;
        foreach (string tag in tagsToSearch)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
            // 各敵の位置を確認
            foreach (GameObject enemy in enemies)
            {
                // x座標を無視してyz平面上の距離を計算
                Vector2 currentPos = new Vector2(currentPosition.y, currentPosition.z);
                Vector2 enemyPos = new Vector2(enemy.transform.position.y, enemy.transform.position.z);

                // 距離を計算
                float distance = Vector2.Distance(currentPos, enemyPos);

                // 最も近い敵を更新
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        if(closestEnemy != null)
        {
            // プレイヤーとの位置差を計算 (yz平面のみ)
            Vector3 directionToEnemy = (closestEnemy.transform.position + Vector3.up * 1f - transform.position).normalized;
    
            // x成分を0にして、yz平面上の方向ベクトルを作成
            directionToEnemy.x = 0f;

            // ボスのx軸をプレイヤーに向けるための角度を計算
            angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.z) * Mathf.Rad2Deg;

            float nangle = -1f * angle;

            // 現在のボスの回転を取得し、x軸のみ回転を変更
            Quaternion targetRotation = Quaternion.Euler(nangle, transform.rotation.y, transform.rotation.z);

            // ボスの回転をx軸方向にのみターンさせる
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, sp2SteerSpeed * Time.fixedDeltaTime);

            // 突進速度を適用
        
            transform.position += transform.forward * chaseSpeed * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.health -= damage;
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("SnakeBody"))
        {
            SnakeBody snakeBody = collision.gameObject.GetComponent<SnakeBody>();
            snakeBody.bodyDamageFlag = true;
            snakeBody.bodyDamage += damage;
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("SnakeHead"))
        {
            Snake3 snake3 = collision.gameObject.GetComponent<Snake3>();
            snake3.health -= damage;
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
