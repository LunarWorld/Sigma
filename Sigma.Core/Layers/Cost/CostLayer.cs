﻿/* 
MIT License

Copyright (c) 2016-2017 Florian Cäsar, Michael Plainer

For full license see LICENSE in the root directory of this project. 
*/

using System;
using Sigma.Core.Handlers;
using Sigma.Core.MathAbstract;
using Sigma.Core.Utils;

namespace Sigma.Core.Layers.Cost
{
	public abstract class CostLayer : BaseLayer
	{
		/// <summary>
		/// Create a base layer with a certain unique name.
		/// </summary>
		/// <param name="name">The unique name of this layer.</param>
		/// <param name="parameters">The parameters to this layer.</param>
		/// <param name="handler">The handler to use for ndarray parameter creation.</param>
		protected CostLayer(string name, IRegistry parameters, IComputationHandler handler) : base(name, parameters, handler)
		{
			ExpectedInputs = new[] { "default", parameters.Get<string>("external_target_alias") };
			ExpectedOutputs = new[] { parameters.Get<string>("external_cost_alias") };
		}

		/// <summary>
		/// Run this layer. Take relevant input values from inputs and put relevant output values in outputs registry. Each input and each output registry represents one connected layer.
		/// </summary>
		/// <param name="buffer">The buffer containing the inputs, parameters and outputs respective to this layer.</param>
		/// <param name="handler">The computation handler to use for computations (duh).</param>
		/// <param name="trainingPass">Indicate whether this is run is part of a training pass.</param>
		public override void Run(ILayerBuffer buffer, IComputationHandler handler, bool trainingPass)
		{
			IRegistry costOutput = buffer.Outputs[buffer.Parameters.Get<string>("external_cost_alias")];
			INDArray predictions = buffer.Inputs["default"].Get<INDArray>("activations");
			INDArray targets = buffer.Inputs[buffer.Parameters.Get<string>("external_target_alias")].Get<INDArray>("activations");

			costOutput["cost"] = CalculateCost(predictions, targets, buffer.Parameters);
			costOutput["importance"] = buffer.Parameters["cost_importance"];
		}

		protected abstract INumber CalculateCost(INDArray predictions, INDArray targets, IRegistry parameters);
	}
}
