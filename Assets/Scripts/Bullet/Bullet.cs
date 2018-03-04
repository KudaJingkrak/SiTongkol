using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Bullet : MonoBehaviour {

    public float speed = 50f;
    public float damage = 1f;
    public float lifetime = float.MaxValue;
    public bool isHoming = false;
    public Transform target;
    SpriteRenderer _sprite;
    Collider2D _collider2D;
    public bool _isActive = false;
    float _speed;
    private bool _isEnable;
    
    void Awake(){
        _sprite = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _speed = speed;
        _isEnable = _isActive;
    }
	
	// Update is called once per frame
	void Update () {        
        if(_isActive){
            if(!_isEnable){
                Enable();
            }
            if(target){
                transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            }else{
                for(int i = 0; i < transform.childCount; i++){
                    Transform child = transform.GetChild(i);
                    if(child.name.Equals("UpTransform")){
                        target = child;
                        break;
                    }
                }
            }

            lifetime -= Time.deltaTime;  
        }

        if(lifetime < 0 || !_isActive){
            Disable();
        }
	}

    public void SetTarget(Transform target){
        if(target){
            this.target = target;
        }
    }

    public void Rotate(float angle){
        transform.Rotate(0f,0f,angle);
    }

    public void AddRotate(float angle){
        transform.Rotate(0f,0f,transform.rotation.z + angle);
    }

    public void Flip(){
        AddRotate(-180f);
    }

    public void SetDirection(Vector2 direction){
        float angle = Vector2.Angle(Vector2.zero, direction.normalized);
        Rotate(angle);
    }

    public void SetDirection(Direction direction){
        switch(direction){
            case Direction.Back:   
                Rotate(0f);
                break;
            case Direction.Right:
                Rotate(90f);
                break;
            case Direction.Front:
                Rotate(180f);
                break;
            case Direction.Left:
                Rotate(270f);
                break;
        }
    }

    public void Disable(){
        _isEnable = false;
        // stop bullet moving
        _speed = 0;
        // disapear sprite
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0f);
        // disable collider
        if(_collider2D.enabled) _collider2D.enabled = false;
    }

    public void Enable(){
        _isEnable = true;
        // set bullet moving
        _speed = speed;
        // appear sprite
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 255f);
        // enable collider
        if(_collider2D.enabled) _collider2D.enabled = true;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
        _speed = speed;
    }

    public float GetCurrentSpeed(){
        return _speed;
    }

    public float GetSpeed(){
        return speed;
    }
}
