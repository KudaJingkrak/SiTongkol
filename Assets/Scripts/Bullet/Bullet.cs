using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Bullet : PoolObject {

    public float speed = 50f;
    public float turnRate = 30f;
    public float damage = 1f;
    public float lifetime = float.MaxValue;
    public bool isHoming = false;
    public Transform target;

    TrailRenderer trail;
    float trailTime;
    SpriteRenderer _sprite;
    Collider2D _collider2D;
    public bool _isActive = false;
    float _speed;
    private Transform _up;
    private bool _isEnable;
    private float _lifetime;
    
    void Awake(){
        trail = GetComponent<TrailRenderer>();
        if(trail)
        {
            trailTime = trail.time;
        }
        _lifetime = lifetime;
        for(int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);
            if(child.CompareTag("Toward")){
                _up = child;
                break;
            }
        }

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
            } else {
                if(isHoming && target){
                Quaternion newRotation  = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
                    newRotation.x = transform.rotation.x;
                    newRotation.y = transform.rotation.y;
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnRate );
                }

                transform.position = Vector2.MoveTowards(transform.position, _up.position, _speed * Time.deltaTime);

                _lifetime -= Time.deltaTime;
                //Debug.Log(_lifetime);
            }
        }

        if(_lifetime < 0 || !_isActive){
            Disable();
        }
	}

    public override void OnObjectReuse(){
        if(trail)
        {
            trail.time = -1;
        }
        
        Invoke("ResetTrail", 0.1f);
        gameObject.SetActive(true);
        Enable();
    }

    protected override void Destroy(){
        Disable();
        gameObject.SetActive(false);
    }

    void ResetTrail(){
        if(trail){
            trail.time = trailTime;
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
        if(trail)
            trail.time = -1;
        _isActive = false;
        _isEnable = false;
        // stop bullet moving
        _speed = 0;
        // disapear sprite
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0f);
        // disable collider
        if(_collider2D.enabled) _collider2D.enabled = false;
    }

    public void Enable(){
        _lifetime = lifetime;
        _isActive = true;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool PosisiDepan;
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Vector2 temp = new Vector2(transform.position.x - collision.gameObject.transform.position.x, transform.position.y - collision.gameObject.transform.position.y);
            temp.Normalize();
            if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
            {
                if (temp.x == 1)
                {
                    if (collision.gameObject.GetComponent<GayatriCharacter>().direction == Direction.Right)
                    {
                        PosisiDepan = true;
                    }
                    else
                    {
                        PosisiDepan = false;
                    }
                    //Kanan
                }
                else
                {
                    if (collision.gameObject.GetComponent<GayatriCharacter>().direction == Direction.Left)
                    {
                        PosisiDepan = true;
                    }
                    else
                    {
                        PosisiDepan = false;
                    }
                    //Kiri
                }
            }
            else
            {
                if (temp.y == 1)
                {
                    if (collision.gameObject.GetComponent<GayatriCharacter>().direction == Direction.Back)
                    {
                        PosisiDepan = true;
                    }
                    else
                    {
                        PosisiDepan = false;
                    }
                    //Belakang
                }
                else
                {
                    if (collision.gameObject.GetComponent<GayatriCharacter>().direction == Direction.Front)
                    {
                        PosisiDepan = true;
                    }
                    else
                    {
                        PosisiDepan = false;
                    }
                    //Depan
                }
            }

            if (PosisiDepan && collision.gameObject.GetComponent<GayatriCharacter>().isReflect)
            {
               gameObject.SetActive(false);
            }
            else
            {
                attackable.ApplyDamage(damage, gameObject);
                gameObject.SetActive(false);
            }
        }
        
        


        //Updated.
        /*
         * Things to do:
         *  - Ngecek posisi dimana bullet itu ketika dibandingkan sama Gayatri
         *  - Normalize
         *  - Dibandingkan dengan Arah Gayatri pada saat itu
         *  - terus kalo iya destroy atau kalo engga apply damage + destroy
         */


        //Previous
        /*
        Debug.Log("masuk ke sini?");
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        if (attackable != null)
        {
            Debug.Log("Testing_Luar");
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<GayatriCharacter>().isReflect)
            {
                Destroy(gameObject);
                return;
            }
            attackable.ApplyDamage(damage);
            //Temporary, nanti ada Poolingnya.
            Destroy(gameObject);
        }
        */
    }
}
