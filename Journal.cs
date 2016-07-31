using System.Collections.Generic/*<List>*/;
using UnityEngine/*<Mathf>*/;

namespace Finegamedesign.Utils
{
	public sealed class Journal
	{
		public string action = null;
		public int milliseconds = 0;
		public float seconds = 0.0f;
		public bool isPlayback = false;
		public int playbackIndex = 0;
		public List<int> playbackDelays;
		public List<string> playbackActions;
		public string commandNow = null;

		public void Update(float deltaSeconds)
		{
			seconds += deltaSeconds;
			milliseconds = (int)(Mathf.Round(seconds * 1000.0f));
			if (isPlayback)
			{
				commandNow = UpdatePlayback();
			}
		}

		private string UpdatePlayback()
		{
			string command = null;
			if (playbackIndex < DataUtil.Length(playbackDelays))
			{
				int delay = playbackDelays[playbackIndex];
				if (delay <= milliseconds)
				{
					command = playbackActions[playbackIndex];
					playbackIndex++;
				}
			}
			return command;
		}

		public void Record(string act)
		{
			action = act;
			seconds = 0.0f;
		}

		public void Read(string historyTsv)
		{
			string[][] table = Toolkit.ParseCsv(historyTsv, "\t");
			if (null == playbackDelays)
			{
				playbackDelays = new List<int>();
			}
			if (null == playbackActions)
			{
				playbackActions = new List<string>();
			}
			DataUtil.Clear(playbackDelays);
			DataUtil.Clear(playbackActions);
			if ("delay" != table[0][0] || "action" != table[0][1])
			{
				throw new System.InvalidOperationException("Expected delay and action as headers."
					+ " Got " + table[0][0] + ", " + table[0][1]);
			}
			for (int rowIndex = 1; rowIndex < DataUtil.Length(table); rowIndex++)
			{
				string[] row = table[rowIndex];
				int delay = Toolkit.ParseInt(row[0]);
				playbackDelays.Add(delay);
				string action = row[1];
				playbackActions.Add(action);
			}
		}
	}
}
