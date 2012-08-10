﻿#region New BSD License

// Copyright (c) 2012, John Batte
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted
// provided that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions
// and the following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions
// and the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

#endregion

using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Moq;

using Patterns.Specifications.Steps.Factories;
using Patterns.Specifications.Subjects;

using TechTalk.SpecFlow;

using MockFactory = Patterns.Specifications.Steps.Factories.MockFactory;

namespace Patterns.Specifications.Steps.Assertions
{
	[Binding]
	public class TestBucketAssertions
	{
		[Then(@"the mocked test bucket's Add method should have been called (.+) time(s)?")]
		public void VerifyTestBucketAdd(int count, string trailingS)
		{
			MockFactory.Mocks.GetMock<ITestBucket>().Verify(bucket => bucket.Add(It.IsAny<TestSubject>()), Times.Exactly(count));
		}

		[Then(@"each item in the test bucket should have a thread Id")]
		public void VerifyTestBucketContentsThreadId()
		{
			TestSubjectFactory.SubjectCollection.Should().OnlyContain(subject => subject.ThreadId.HasValue);
		}

		[Then(@"there should be more than one distinct thread Id in the test bucket's items")]
		public void VerifyTestBucketContentsMultipleThreadIds()
		{
			IEnumerable<int?> distinctIds = TestSubjectFactory.SubjectCollection.Select(subject => subject.ThreadId).Distinct();
			distinctIds.Should().HaveCount(count => count > 1);
		}
	}
}