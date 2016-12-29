﻿using NUnit.Framework;
using Sigma.Core.Handlers;
using Sigma.Core.MathAbstract;
using Sigma.Core.Training.Initialisers;
using System;
using Sigma.Core.Handlers.Backends.SigmaDiff.NativeCpu;
using Sigma.Core.MathAbstract.Backends.DiffSharp;

namespace Sigma.Tests.Training.Initialisers
{
	public class TestGaussianInitialiser
	{
		[TestCase]
		public void TestGaussianInitialiserInitialise()
		{
			ConstantValueInitialiser initialiser = new ConstantValueInitialiser(2.0);

			INDArray array = new ADNDArray<float>(2, 1, 2, 2);
			IComputationHandler handler = new CpuFloat32Handler();
			Random random = new Random();

			Assert.Throws<ArgumentNullException>(() => initialiser.Initialise(null, handler, random));
			Assert.Throws<ArgumentNullException>(() => initialiser.Initialise(array, null, random));
			Assert.Throws<ArgumentNullException>(() => initialiser.Initialise(array, handler, null));


		}
	}
}
