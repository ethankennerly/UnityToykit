using System.Collections.Generic;

public class ViewModel
{
	public IModel model;
	public bool isVerbose = false;
	public string main = "Main";
	public string mouseDown = "";
	public string inputString = "";

	public Dictionary<string, object> graph = new Dictionary<string, object>(){};

	public string[] buttons = new string[]{
	};

	public string[] sounds = new string[]{
	};

	public List<string> soundNews = new List<string>();

	public Dictionary<string, object> news = new Dictionary<string, object>();

	public void SetText(string[] address, string text)
	{
		ControllerUtil.SetNews(news, address, text, "text");
	}

	public void SetState(string[] address, string state)
	{
		ControllerUtil.SetNews(news, address, state);
	}

	public void Start()
	{
		model.SetViewModel(this);
		model.Start();
	}

	public void Update(float deltaTime)
	{
		model.Update(deltaTime);
		inputString = "";
		mouseDown = "";
	}

	public void OnMouseDown(string name)
	{
		if (isVerbose) {
			Toolkit.Log("OnMouseDown: " + name);
		}
		mouseDown = name;
	}

	public void InputString(string input)
	{
		if (isVerbose) {
			Toolkit.Log("InputString: " + input);
		}
		inputString = input;
	}
}
