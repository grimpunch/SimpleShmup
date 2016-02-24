using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealthBarUIHandler : MonoBehaviour
{

	public GameObject bossHealthBar;
	public EnemyHitHandler bossHitHandler;
	public RectTransform bossHealthBarRect;
	public int startHealth;
	public int currentHealth;
	// Use this for initialization
	void Start()
	{
		bossHealthBar = GameObject.Find("BossHealthSlider");
		bossHitHandler = gameObject.GetComponent<EnemyHitHandler>();
		bossHealthBarRect = bossHealthBar.GetComponent<RectTransform>();
		startHealth = bossHitHandler.shipHealth;
		bossHealthBar.GetComponent<Slider>().maxValue = startHealth;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (bossHitHandler.enabled) {
			bossHealthBarRect.anchoredPosition = new Vector2(bossHealthBarRect.anchoredPosition.x, -20f);
			bossHealthBar.GetComponent<Slider>().value = bossHitHandler.shipHealth;
			if (bossHealthBar.GetComponent<Slider>().value <= 0) {
				bossHealthBarRect.anchoredPosition = new Vector2(bossHealthBarRect.anchoredPosition.x, 20f);
			}
		}
	}
}
