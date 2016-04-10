public class ModelExample : IModel
{
	private ViewModel view;

	public void SetViewModel(ViewModel viewModel)
	{
		view = viewModel;
	}

	public void Start()
	{
	}

	public void Update(float deltaTime)
	{
		if ("" != view.inputString) {
			Toolkit.Log("ModelExample: input <" 
				+ view.inputString + ">");
		}
	}
}
