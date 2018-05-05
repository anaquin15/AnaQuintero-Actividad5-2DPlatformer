using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para usar el UI

public class CharacterControl : MonoBehaviour {

	public float speed;
	int coins = 0; // cantidad de monedas
	public Text contadorCoins;
	int heart = 0; // cantidad de vidas
	public Text contadorHeart;
	int star = 0; // cantidad de estrellas
	public Text contadorStar;
	public AudioClip coin;
	public AudioClip life;
	public AudioClip stars;
	public AudioClip muerte;
	public AudioClip finals;
	public AudioClip LoseLife;

	bool isGrounded =false;

	Animator anim;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent <AudioSource> ();
		anim = GetComponent <Animator> ();
	}

	public void clickEnElBoton() {
		this.gameObject.GetComponent <Rigidbody2D> ().AddForce (Vector2.up * 7, ForceMode2D.Impulse); // permite saltar presionando el boton de la pantalla
	}
	
	// Update is called once per frame
	void Update () {
		//en el componente de rigidbody 2d tenemos que activar el constraint en el eje Z para que no rote y el movimiento quede bien
		//movimiento a la derecha
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.gameObject.transform.Translate (speed * Time.deltaTime, 0, 0);
			//this.gameObject.transform.Translate (Vector2.left * -speed * Time.deltaTime, Space.World); // esta es otra opcion
		}
		//movimiento a la izquierda
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.gameObject.transform.Translate (-speed * Time.deltaTime, 0, 0);
			//this.gameObject.transform.Translate (Vector2.right * -speed * Time.deltaTime, Space.World); // esta es otra opcion
		} 
		//salto
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {
			this.gameObject.GetComponent <Rigidbody2D> ().AddForce (Vector2.up * 9, ForceMode2D.Impulse); // le agregamos una fuerza hacia arriba
		}
		
	}
	//Cuando el collider 2D del gameObject colisiona con otro collider 2D
	// Es muy importante que sea 2D. Si es 3D no se detecta

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Coin") { //Detectamos colision solo con monedas
			// aumentar la cantidad de monedas
			coins = coins + 1;
			// mostramos la cantidad de monedas usando el componente Text
			contadorCoins.text = coins.ToString ();

			audio.PlayOneShot (coin, 9);

			//Destruimos la moneda
			GameObject.Destroy (coll.gameObject);
		}

			
		if (coll.gameObject.tag == "Heart") { //Detectamos colision solo con Vidas
			// aumentar la cantidad de vidas
			heart = heart + 1;
			// mostramos la cantidad de vidas usando el componente Text
			contadorHeart.text = heart.ToString ();

			audio.PlayOneShot (life, 9);

			//Destruimos la moneda
			GameObject.Destroy (coll.gameObject);
		}


		if (coll.gameObject.tag == "Enemies") { //Detectamos colision solo con Vidas
			// aumentar la cantidad de vidas
			heart = heart - 1;
			// mostramos la cantidad de vidas usando el componente Text
			contadorHeart.text = heart.ToString ();


			audio.PlayOneShot (LoseLife, 9);

			//Destruimos la moneda
			GameObject.Destroy (coll.gameObject);
		}

		if (coll.gameObject.tag == "Stars") { //Detectamos colision solo con estrellas
			// aumentar la cantidad de estrellas
			star = star + 1;
			// mostramos la cantidad de estrellasusando el componente Text
			contadorStar.text = star.ToString ();

			audio.PlayOneShot (stars, 9);

			//Destruimos la Estrella
			GameObject.Destroy (coll.gameObject);
		}
			
		if (coll.gameObject.tag == "caida") {
			audio.PlayOneShot (muerte, 8);
		}
		if (coll.gameObject.tag == "final") {
			audio.PlayOneShot (finals, 8);
		}
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.collider.gameObject.tag == "isGrounded") {
			isGrounded = true;


		}
	}
	void OnCollisionExit2D (Collision2D collision){
		if (collision.collider.gameObject.tag == "isGrounded") {
			isGrounded = false;

		}
	}



}
