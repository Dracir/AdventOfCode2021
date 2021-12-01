using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public static class GameOfLife
{
	public static Dictionary<int[], T> DoStepOfLife<T>(MultiDimentionalArray<T> array, StepOfLifeData<T> data)
	{
		var expendingAmount = data.Expending ? 1 : 0;
		var minIndex = array.MinKeys.Select(x => x - expendingAmount).ToArray();
		var maxIndex = array.MaxKeys.Select(x => x + expendingAmount).ToArray();

		var changes = new Dictionary<int[], T>();

		foreach (var point in array.PointsAndValues(minIndex, maxIndex))
		{
			var nbActiveNeighbors = 0;
			foreach (var neighbor in data.GetNeighbors(array, point.Key))
				if (data.IsActive(neighbor.Value)) nbActiveNeighbors++;

			var newState = data.GetNextState(point.Value, nbActiveNeighbors);

			if (point.Value != null && data.IsActive(point.Value) != data.IsActive(newState))
				changes.Add(point.Key, newState);
		}

		return changes;
	}

	public struct StepOfLifeData<T>
	{
		public bool Expending;
		public Func<T, bool> IsActive;
		public GetNeighborsDelegate GetNeighbors;
		public GetNextStateDelegate GetNextState;

		public delegate T GetNextStateDelegate(T? oldState, int nbNeighbors);
		public delegate KeyValuePair<int[], T>[] GetNeighborsDelegate(MultiDimentionalArray<T> array, int[] position);

		public StepOfLifeData(bool expending, Func<T, bool> tileActive, GetNeighborsDelegate getNeighbors, GetNextStateDelegate getNextState)
		{
			Expending = expending;
			IsActive = tileActive;
			GetNeighbors = getNeighbors;
			GetNextState = getNextState;
		}
	}
}