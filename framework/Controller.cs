public class Controller
{
	public ViewModel viewModel = new ViewModel();
	public View view = new View();

	public void SetModel(IModel model)
	{
		viewModel.model = model;
	}

	public void Start()
	{
		viewModel.Start();
		view.graph = ControllerUtil.FindGraphByName(viewModel.graph, viewModel.main);
		ViewUtil.SetupAudio(viewModel.main, viewModel.sounds);
		ControllerUtil.SetupButtons(this, viewModel.buttons);
	}

	private void UpdateInput()
	{
		string input = ViewUtil.GetInputString();
		input = Toolkit.NormalizeLines(input);
		if (null != input && "" != input) {
			viewModel.InputString(input);
		}
	}

	public void Update(float deltaTime)
	{
		UpdateInput();
		viewModel.Update(deltaTime);
		ControllerUtil.SetStates(viewModel.news, view.graph);
		ControllerUtil.PlaySounds(viewModel.soundNews);
	}

	public void OnMouseDown(string name)
	{
		viewModel.OnMouseDown(name);
	}
}
