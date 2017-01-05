﻿/* 
MIT License

Copyright (c) 2016-2017 Florian Cäsar, Michael Plainer

For full license see LICENSE in the root directory of this project. 
*/

using Sigma.Core.Architecture;
using Sigma.Core.Handlers;

namespace Sigma.Core.Training.Operators.Workers
{
	/// <summary>
	///     A single worker which directly executes the training process defined in a trainer and is spawned by an operator to
	///     complete some part of a training task.
	/// 
	///     Typically multiple workers are used simultaneously and then their individual copies of the trained models are
	///     merged at certain intervals for optimal device usage.
	/// </summary>
	public interface IWorker
	{
		/// <summary>
		///     The operator which controls this worker.
		/// </summary>
		IOperator Operator { get; }

		/// <summary>
		///     The current state of the worker. <see cref="ExecutionState.None" />
		///     if the worker has not been started yet;
		/// </summary>
		ExecutionState State { get; }

		/// <summary>
		///     The computation handler to use for computation and ndarray management.
		/// </summary>
		IComputationHandler Handler { get; }

		/// <summary>
		///     A local copy of the network (model) to train. Used to enable parallel network training.
		/// </summary>
		INetwork LocalNetwork { get; set; }

		/// <summary>
		///     The current iteration number (i.e. how many iterations have been executed on this worker).
		/// </summary>
		int LocalIterationNumber { get; }

		/// <summary>
		///     Start this worker and start training the network as defined in the trainer and ordered by the operator.
		/// </summary>
		void Start();

		/// <summary>
		///		Start this worker for one iteration with the parameters defined in the trainer.
		///		All the initialisation happens here. 
		/// </summary>
		void RunTrainingIteration();

		/// <summary>
		///     Signal this worker to pause at the next opportunity (after an iteration).
		/// </summary>
		void SignalPause();

		/// <summary>
		///     Signal this worker to resume the work.
		/// </summary>
		void SignalResume();

		/// <summary>
		///     Signal this worker to stop the execution as soon as possible.
		/// </summary>
		void SignalStop();
	}
}