using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Arah_Bullet arahPeluru;
	// Use this for initialization
	void Start () {
        arahPeluru = Arah_Bullet.diam;
	}
	
	// Update is called once per frame
	void Update () {
        switch (arahPeluru)
        {
            //pakai transform apa pakai addforce ya?
            case Arah_Bullet.atas:
                transform.Translate(Vector2.up);
                //apa yang terjadi kalo arah bulletnya ke atas
                break;
            case Arah_Bullet.bawah:
                transform.Translate(Vector2.down);
                //apa yang terjadi kalo arah bullet ke bawah
                break;
            case Arah_Bullet.kiri:
                transform.Translate(Vector2.left);
                //apa yang terjadi kalo arah bullet ke kiri
                break;
            case Arah_Bullet.kanan:
                transform.Translate(Vector2.right);
                //apa yang terjadi kalo arah bullet ke kanan
                break;
            case Arah_Bullet.diam:
                //apa yang terjadi kalo arah bullet diam
                break;
            default:
                print("Error in Enum Arah Bullet");
                break;
        }
	}
}
