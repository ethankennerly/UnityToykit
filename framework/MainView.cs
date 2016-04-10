using UnityEngine;  // MonoBehaviour

public class MainView : MonoBehaviour
{
	// Set your game's model
	// Extend or copy to make your own MainView class.
	// See Examples/MainExample
	public Controller controller = new Controller();

	public virtual void Start()
	{
		controller.Start();
	}
	
	public virtual void Update()
	{
		controller.Update(Time.deltaTime);
	}
}
