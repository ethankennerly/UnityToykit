public class MainExample : MainView
{
	public override void Start()
	{
		controller.SetModel(new ModelExample());
		base.Start();
	}
}
